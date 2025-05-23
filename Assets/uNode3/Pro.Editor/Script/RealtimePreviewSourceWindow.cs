using UnityEngine;
using UnityEditor;
using System;
using System.Threading;
using System.Collections.Generic;

namespace MaxyGames.UNode.Editors {
	public class RealtimePreviewSourceWindow : PreviewSourceWindow {
		private static RealtimePreviewSourceWindow window;

		private string source;
		private List<Exception> exception;
		public float refreshTime;
		public bool sourceChanged;
		Vector2 exceptionScroll;

		public static void ShowWindow() {
			window = GetWindow(typeof(RealtimePreviewSourceWindow), false) as RealtimePreviewSourceWindow;
			window.minSize = new Vector2(300, 200);
			window.focus = true;
			window.autoRepaintOnSceneChange = true;
			window.titleContent = new GUIContent("C# Preview", uNodeEditorUtility.GetTypeIcon(typeof(TypeIcons.ScriptCodeIcon)) as Texture2D);
			window.Show();
		}

		private void Update() {
			Repaint();
		}


		void OnGUI() {
			if(prevHeighlight != source) {
				lock(lockHighlight) {
					if(!sourceIsHighligted && (finishHeighlight || highlightThread == null)) {
						HighlightSyntax();
					}
				}
			}
			if(source != null) {
				if(sourceChanged && Event.current.type != EventType.Repaint) {
					lines = source.Split('\n');
					pureLines = script.Split('\n');
					sourceChanged = false;
				}
				GUI.Box(new Rect(-10, -10, position.width + 100, position.height + 100), "");
				DrawToolbar();
				scrollPos = EditorGUILayout.BeginScrollView(scrollPos);
				ShowPreviewSource(lines, pureLines, informations, ref selectedInfos);
				EditorGUILayout.EndScrollView();
				DrawErrors();
				HandleEvents();
			}
			if(exception != null && exception.Count > 0) {
				exceptionScroll = EditorGUILayout.BeginScrollView(exceptionScroll);
				foreach(var ex in exception) {
					EditorGUILayout.HelpBox(ex.ToString(), MessageType.Error, true);
				}
				EditorGUILayout.EndScrollView();
			}
			if(!focus) {
				GUI.FocusControl(null);
				EditorGUI.FocusTextInControl(null);
				focus = true;
			}
			Repaint();
		}

		object lockHighlight = new object();
		Thread highlightThread;
		bool finishHeighlight;
		bool sourceIsHighligted;
		string prevHeighlight;

		private void HighlightSyntax() {
			if(highlightThread == null) {
				finishHeighlight = false;
				prevHeighlight = source;
				highlightThread = new Thread(new ParameterizedThreadStart(DoHighlightSyntax));
				highlightThread.Name = "HighlightSyntaxThread";
				highlightThread.IsBackground = true;
				highlightThread.Start(source);
			}
		}

		void DoHighlightSyntax(object obj) {
			try {
				var syntaxHighlighter = EditorBinding.syntaxHighlighter;
				if(syntaxHighlighter != null) {
					string prevSource = obj as string;
					string highlight = syntaxHighlighter.GetMethod("GetRichTextAsync").InvokeOptimized(null, new object[] { prevSource }) as string;
					lock(lockHighlight) {
						if(!string.IsNullOrEmpty(highlight) && prevSource == obj as string) {
							source = highlight;
							sourceChanged = true;
							sourceIsHighligted = true;
						}
					}
				}
				else {
					lock(lockHighlight) {
						sourceIsHighligted = true;
					}
				}
			}
			catch { }
			lock(lockHighlight) {
				finishHeighlight = true;
				highlightThread = null;
			}
		}

		void PrepareGenerate() {
			uNodeEditor editor = uNodeEditor.window;
			if(editor != null) {
				DoGenerate(editor.selectedTab.owner);
			}
			Repaint();
		}

		protected override void OnEnable() {
			base.OnEnable();
			uNodeEditor.onChanged -= PrepareGenerate;
			uNodeEditor.onChanged += PrepareGenerate;
			PrepareGenerate();
		}

		protected override void OnDisable() {
			base.OnDisable();
			uNodeEditor.onChanged -= PrepareGenerate;
		}

		private object lockThread = new object();
		private Thread thread;
		//private bool hasFinishedGenerate = true;
		private UnityEngine.Object currGO;

		void ResetThread() {
			TerminateThread();
			if(thread == null) {
				lock(lockThread) {
					currGO = null;
					//hasFinishedGenerate = false;
				}
			}
		}

		void DoGenerate(UnityEngine.Object graph) {
			ResetThread();
			if(thread == null && graph != null) {
				currGO = graph;
				thread = new Thread(new ParameterizedThreadStart(Generate));
				thread.Name = "GenerateThread";
				thread.IsBackground = true;
				thread.Start(graph);
			}
		}

		void TerminateThread() {
			if(thread != null && thread.IsAlive) {
				thread.Abort();
				thread.Join();
				thread = null;
			}
		}

		void Generate(object obj) {
			try {
				uNodeThreadUtility.WaitOneFrame();
				uNodeThreadUtility.WaitOneFrame();
				var generatorData = uNodePreference.GetPreference().generatorData;
				var script = CG.Generate(new CG.GeneratorSetting(obj as UnityEngine.Object) {
					debugScript = false,
					isAsync = true,
					isPreview = true,
					includeGraphInformation = true,
					fullTypeName = true,
					maxQueue = 15,
					fullComment = generatorData.fullComment,
				});
				lock(lockThread) {
					exception = null;
					//this.script = script;
					uNodeThreadUtility.Queue(() => {
						if(script == null)
							return;
						List<ScriptInformation> informations;
						string s = script.ToScript(out informations);
						if(sourceIsHighligted && prevHeighlight == s) {

						}
						else {
							selectedInfos = null;
							source = s;
							sourceChanged = true;
							sourceIsHighligted = false;
							compileResult = null;
						}
						if(script.hasError) {
							exception = script.errors;
							compileResult = null;
						}
						this.informations = informations.ToArray();
						this.script = s;
					});
					uNodeThreadUtility.WaitOneFrame();
				}
			}
			catch(System.Exception ex) {
				if(!(ex is ThreadAbortException)) {
					lock(lockThread) {
						exception = new List<Exception>() { ex };
					}
				}
			}
			//hasFinishedGenerate = true;
			thread = null;
		}
	}
}