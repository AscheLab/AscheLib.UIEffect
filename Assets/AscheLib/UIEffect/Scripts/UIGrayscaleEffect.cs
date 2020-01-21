using UnityEngine;
using UnityEngine.UI;

namespace AscheLib.UI {
	[ExecuteInEditMode]
	[RequireComponent(typeof(Graphic))]
	[DisallowMultipleComponent]
	[AddComponentMenu("UI/Effects/UIGrayscaleEffect")]
	class UIGrayscaleEffect : UIGrabPassEffectBase {
		// しきい値
		[SerializeField] [Range(0, 1)] private float _threshold = 1;
		public float threshold {
			get { return _threshold; }
			set { _threshold = value; SetDirty(); }
		}

		// 使用するシェーダー名
		protected override string shaderName => "Hidden/UI/GrabPass/Grayscale";

		// SetFloatだとMaskの下にある時に値を変えても適用されない為、Textureの色として情報を渡す
		protected override Color GetParameterColor () {
			return new Color(_threshold, 0, 0, 1);
		}
	}
}
