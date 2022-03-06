using UnityEngine;

namespace Materials {
    
    [ExecuteInEditMode]
    public class SpriteOutline : MonoBehaviour {
        public Color color = Color.white;
        public bool outline;
        [Range(0, 16)]
        public int outlineSize = 1;
    
        private SpriteRenderer _spriteRenderer;
        private static readonly int Outline = Shader.PropertyToID("_Outline");
        private static readonly int OutlineColor = Shader.PropertyToID("_OutlineColor");

        private void Start() {
            _spriteRenderer = GetComponent<SpriteRenderer>();
        }

        private void Update() {
            MaterialPropertyBlock mpb = new MaterialPropertyBlock();
            _spriteRenderer.GetPropertyBlock(mpb);
            mpb.SetFloat(Outline, outline ? 1f : 0);
            mpb.SetColor(OutlineColor, color);
            mpb.SetFloat("_OutlineSize", outlineSize);
            _spriteRenderer.SetPropertyBlock(mpb);
        }
    }
}