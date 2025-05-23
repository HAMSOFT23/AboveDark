﻿using UnityEditor;
using UnityEngine;
using System.Collections.Generic;
using UnityEditor.IMGUI.Controls;

namespace MaxyGames.UNode.Editors {
	public class GraphHierarchy : EditorWindow {
		[System.Serializable]
		class GraphHierarchyState : TreeViewState {

		}

		[SerializeField] GraphHierarchyState state;
		public GraphHierarchyTree treeView;

		//public GraphEditorData data = new GraphEditorData();

		public static void ShowWindow() {
			GraphHierarchy window = (GraphHierarchy)GetWindow(typeof(GraphHierarchy));
			window.autoRepaintOnSceneChange = true;
			window.wantsMouseMove = true;
			window.titleContent = new GUIContent("Graph Hierarchy");
			window.Show();
		}

		void OnEnable() {
			if(state == null)
				state = new GraphHierarchyState();
			treeView = new GraphHierarchyTree(state);
			wantsMouseEnterLeaveWindow = true;
			uNodeGUIUtility.onGUIChanged -= ReloadTree;
			uNodeGUIUtility.onGUIChanged += ReloadTree;
		}

		void OnDisable() {
			uNodeGUIUtility.onGUIChanged -= ReloadTree;
		}

		void ReloadTree(object obj, UIChangeType changeType) {
			switch(changeType) {
				case UIChangeType.Small:
				case UIChangeType.Average:
				case UIChangeType.Important:
					ReloadTree();
					break;
			}
		}

		void ReloadTree() {
			treeView.Reload(true);
		}

		void DoTreeView(Rect rect) {
			treeView.OnGUI(rect);
		}

		public void OnGUI() {
			uNodeEditor editor = uNodeEditor.window;
			if(editor == null) {
				EditorGUILayout.HelpBox("uNode Editor is not opened", MessageType.Warning);
				return;
			} else if(editor.graphData.graph == null) {
				EditorGUILayout.HelpBox("No opened graph on uNode Editor", MessageType.Warning);
				return;
			}
			{
				Rect areaRect = new Rect(0, 0, position.width, position.height);
				//if(position.width >= 500) {
				//	areaRect = new Rect(0, 0, position.width - 300, position.height);
				//}
				GUILayout.BeginArea(areaRect);
				DoTreeView(areaRect);
				GUILayout.EndArea();
			}
			//if(position.width >= 500) {
			//	Rect areaRect = new Rect(position.width - 300, 0, 300, position.height);
			//	GUI.Box(new Rect(areaRect.x, areaRect.y, areaRect.width + 20, areaRect.height), "", "Box");
			//	GUILayout.BeginArea(areaRect);
			//	if(data.selected != null) {
			//		CustomInspector.ShowInspector(data);
			//	}
			//	GUILayout.EndArea();
			//}
			//if(GUI.changed) {
			//	if(uNodeEditor.window != null) {
			//		uNodeEditor.GUIChanged();
			//	}
			//}
		}

		private void OnSelectionChange() {
			Repaint();
			ReloadTree();
		}
	}
}