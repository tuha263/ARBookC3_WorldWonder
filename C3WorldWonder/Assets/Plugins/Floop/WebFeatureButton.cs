using UnityEngine;
using System.Collections;


namespace Floop 
{

	public abstract class WebFeatureButton : MonoBehaviour 
	{
		const int HighDpiTreshold = 320;
		
		public enum PlacementAnchor
		{
			TopLeft,
			TopRight,
			BottomLeft,
			BottomRight
		}
		
		public PlacementAnchor Anchor = PlacementAnchor.BottomRight;
		public Vector2 Offset = new Vector2{ x = 8, y = 8 };
		public Vector2 MaxSize = new Vector2{ x = 64, y = 64 };

		protected abstract WebFeature CreateWebFeature(); 
		
		private Texture2D buttonIcon;
		private WebFeature webFeature;
		bool isShowingFeature;
		bool inSetup;

		void Awake()
		{
			if (Screen.dpi >= HighDpiTreshold)
			{
				AdjustDimensionsForHighDpiScreens();
			}
		}
	
		void Start ()
		{
			Setup();
		}


		void AdjustDimensionsForHighDpiScreens()
		{
			MaxSize = ToHighDpi(MaxSize);
			Offset = ToHighDpi(Offset);
		}
		
		Vector2 ToHighDpi(Vector2 vector) 
		{
			return new Vector2 { x = vector.x * 2, y = vector.y * 2 };
		}
		
		void OnEnable()
		{
			if (null == this.buttonIcon)
			{
				Setup();
			}
		}
		
		void Setup()
		{
			this.enabled = false;
			if (!inSetup)
			{
				inSetup = true;

				if(null == webFeature)
				{
					webFeature = CreateWebFeature();
				}

				FloopManager.Instance.GetWebFeatureLauncherImage(context: this, feature:this.webFeature, callback:(Texture2D icon) => {
					
					inSetup = false;

					FloopLogger.Debug("GetWebFeatureLauncherImage for {0} returned with {1}", this.webFeature, icon);
					
					if (null != icon)
					{
						this.buttonIcon = icon;
						this.enabled = true;
						ResizeTextureToFit(texture:icon, maxWidth:MaxSize.x, maxHeight:MaxSize.y);
					}
				});
				
			}
		}
		
		
		static void ResizeTextureToFit(Texture2D texture, float maxWidth, float maxHeight)
		{
			if (texture.width > maxWidth || texture.height > maxHeight) 
			{
				float widthScale = maxWidth / (float)texture.width;
				float heightScale = maxHeight / (float)texture.height;
				float scale = Mathf.Min(widthScale, heightScale);
				
				int newWidth = (int)(scale * texture.width);
				int newHeight = (int)(scale * texture.height);
				
				FloopLogger.Debug("resizing texture of size ({0}, {1}) to ({2}, {3}), from scaleFactor: {4}; max:({5},{6})",
				                  texture.width,texture.height, newWidth, newHeight, scale, maxWidth, maxHeight
				                  );
				
				TextureScale.Bilinear(tex:texture, newWidth:newWidth, newHeight:newHeight);
			}
		}
		
		
		void OnGUI()
		{
			if (null != this.buttonIcon)
			{
				ShowGUIButton(EnsureShowingFeature);
			}
			
		}	
		
		void ShowGUIButton(System.Action callback)
		{
			GUIStyle style = new GUIStyle();
			style.border = new RectOffset(0,0,0,0);
			
			var origBgCol = GUI.backgroundColor;
			
			try
			{
				GUI.backgroundColor = new Color(0,0,0,0);
				Rect bounds = CalcButtonBounds();
				if (GUI.Button(bounds, buttonIcon, style))
				{
					callback();
				}
			}
			finally
			{
				GUI.backgroundColor = origBgCol;
			}
		}
		
		Rect CalcButtonBounds()
		{
			Vector2 iconSize = new Vector2(this.buttonIcon.width, this.buttonIcon.height);
			Rect bounds = GetBoundsOnScreenFromUserParams(iconSize);
			return bounds;
		}
		
		
		Rect GetBoundsOnScreenFromUserParams(Vector2 iconSize)
		{
			Vector2 origin;
			
			float xFromLeft = Offset.x;
			float xFromRight = Camera.main.pixelWidth - iconSize.x - Offset.x;
			float yFromTop = Offset.y;
			float yFromBottom = Camera.main.pixelHeight - iconSize.y - Offset.y;
			
			switch(Anchor)
			{
			case PlacementAnchor.TopLeft:
				origin = new Vector2(xFromLeft, yFromTop);
				break;
			case PlacementAnchor.TopRight:
				origin = new Vector2(xFromRight, yFromTop);
				break;
			case PlacementAnchor.BottomLeft:
				origin = new Vector2(xFromLeft, yFromBottom);
				break;
			case PlacementAnchor.BottomRight:
				origin = new Vector2(xFromRight, yFromBottom);
				break;
			default:
				throw new UnityException("Unhandled anchor type: " + Anchor);
			}
			
			Rect bounds = new Rect(origin.x, origin.y, iconSize.x, iconSize.y);
			
			return bounds;
		}
		
		void EnsureShowingFeature()
		{
			if (!isShowingFeature)
			{
				isShowingFeature = true;

				FloopManager.Instance.ShowWebFeature(this.webFeature, (result) => {
					isShowingFeature = false;
					FloopLogger.Info("{0} returned with {1}", webFeature.Description, result);
				});
			}
		}

		
	}

}
