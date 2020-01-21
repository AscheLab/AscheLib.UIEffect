using UnityEngine;
using UnityEngine.UI;

namespace AscheLib.UI {
	[ExecuteInEditMode]
	[RequireComponent(typeof(Graphic))]
	[DisallowMultipleComponent]
	[AddComponentMenu("UI/Effects/UIFrostedGlassEffect")]
	class UIFrostedGlassEffect : UIGrabPassEffectBase {
		// ぼやける強さ
		[SerializeField] [Range(0, 5)] private float _factor = 1;
		public float factor {
			get { return _factor; }
			set { _factor = value; SetDirty(); }
		}

		// 使用するシェーダー名
		protected override string shaderName => "Hidden/UI/GrabPass/FrostedGlass";

		// SetFloatだとMaskの下にある時に値を変えても適用されない為、Textureの色として情報を渡す
		protected override Color GetParameterColor () {
			return new Color(_factor / 5, 0, 0, 1);
		}
	}
}
