﻿namespace MaxyGames.UNode.Transition {
	[TransitionMenu("OnTriggerStay2D", "OnTriggerStay2D")]
	public class OnTriggerStay2D : TransitionEvent {
		[Filter(typeof(UnityEngine.Collider2D), SetMember = true)]
		public MemberData storeCollider = new MemberData();

		public override void OnEnter(Flow flow) {
			UEvent.Register<UnityEngine.Collider2D>(UEventID.OnTriggerStay2D, flow.target as UnityEngine.Component, (value) => Execute(flow, value));
		}

		public override void OnExit(Flow flow) {
			UEvent.Unregister<UnityEngine.Collider2D>(UEventID.OnTriggerStay2D, flow.target as UnityEngine.Component, (value) => Execute(flow, value));
		}


		void Execute(Flow flow, UnityEngine.Collider2D collider) {
			if(storeCollider.isAssigned) {
				storeCollider.Set(flow, collider);
			}
			Finish(flow);
		}

		public override string GenerateOnEnterCode() {
			if(!CG.HasInitialized(this)) {
				CG.SetInitialized(this);
				var mData = CG.generatorData.GetMethodData("OnTriggerStay2D");
				if(mData == null) {
					mData = CG.generatorData.AddMethod(
						"OnTriggerStay2D",
						typeof(void),
						typeof(UnityEngine.Collider2D));
				}
				string set = null;
				if(storeCollider.isAssigned) {
					set = CG.Set(CG.Value((object)storeCollider), mData.parameters[0].name).AddLineInEnd();
				}
				mData.AddCode(
					CG.Condition(
						"if",
						CG.CompareNodeState(enter, null),
						set + CG.FlowTransitionFinish(this)
					)
				);
			}
			return null;
		}
	}
}
