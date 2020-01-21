using UnityEngine;
using UnityEngine.UI;

namespace AscheLib.UI {
	[ExecuteInEditMode]
	[RequireComponent(typeof(Graphic))]
	[DisallowMultipleComponent]
	[AddComponentMenu("UI/Effects/UIMosaicEffect")]
	class UIMosaicEffect : UIGrabPassEffectBase {
		// 分割数
		[SerializeField] [Range(0, 1000)] private int _blockNum = 10;
		public int blockNum {
			get { return _blockNum; }
			set { _blockNum = value; SetDirty(); }
		}

		// 使用するシェーダー名
		protected override string shaderName => "Hidden/UI/GrabPass/Mosaic";

		// SetFloatだとMaskの下にある時に値を変えても適用されない為、Textureの色として情報を渡す
		protected override Color GetParameterColor () {
			return new Color(_blockNum / 1000.0f, 0, 0, 1);
		}
	}
}
