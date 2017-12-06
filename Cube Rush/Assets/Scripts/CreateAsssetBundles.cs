using UnityEditor;

public class CreateAsssetBundles  {

	[MenuItem ("Assets/Build AssetBundles") ]
	static void BuildAllAssetBundles ()
	{
		BuildPipeline.BuildAssetBundles ("Assets/AssetBundles", BuildAssetBundleOptions.None, BuildTarget.StandaloneOSXUniversal);

	}
}
