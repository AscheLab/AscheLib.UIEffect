using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

namespace AscheLib.UI {
	[ExecuteInEditMode]
	[RequireComponent(typeof(Graphic))]
	[DisallowMultipleComponent]
	abstract class UIGrabPassEffectBase : UIBehaviour {
		Graphic _graphic;
		CanvasRenderer _canvasRenderer;
		RectTransform _rectTransform;
		Material _targetMaterial;

		// SetFloatだとMaskの下にある時に値を変えても適用されない為、Textureの色として情報を渡す
		Texture2D _parameterTexture = null;
		int _propertyId = -1;

		protected abstract string shaderName { get; }
		protected virtual string parameterName { get { return "_ParameterTexture"; } }

		public Graphic targetGraphic {
			get {
				if (_graphic == null) {
					_graphic = GetComponent<Graphic>();
				}
				return _graphic;
			}
		}
		public Material targetMaterial {
			get {
				if (_targetMaterial == null) {
					_targetMaterial = new Material(Shader.Find(shaderName));
				}
				return _targetMaterial;
			}
		}

		protected override void OnEnable () {
			base.OnEnable();
			ModifyMaterial();
			SetDirty();
		}

		protected override void OnDisable () {
			base.OnDisable();
			ModifyMaterial();
		}

		protected override void OnValidate () {
			base.OnValidate();
			ModifyMaterial();
			SetDirty();
		}

		protected void SetDirty () {
			if (_propertyId == -1) {
				_propertyId = Shader.PropertyToID(parameterName);
			}
			if (_parameterTexture == null) {
				_parameterTexture = new Texture2D(2, 2, TextureFormat.ARGB32, false);
				targetMaterial.SetTexture(_propertyId, _parameterTexture);
			}
			var color = GetParameterColor();
			for (int y = 0; y < _parameterTexture.height; y++) {
				for (int x = 0; x < _parameterTexture.width; x++) {
					_parameterTexture.SetPixel(x, y, color);
				}
			}
			_parameterTexture.Apply();
		}

		public void ModifyMaterial () {
			targetGraphic.material = isActiveAndEnabled ? targetMaterial : null;
		}

		protected abstract Color GetParameterColor ();
	}
}
