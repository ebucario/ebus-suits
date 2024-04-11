using ModelReplacement;
using UnityEngine;

namespace EbusSuits
{
    public class MREBU : BodyReplacementBase
    {
        protected override GameObject LoadAssetsAndReturnModel()
        {
            string model_name = "ebu";
            return Assets.MainAssetBundle.LoadAsset<GameObject>(model_name);
        }
    }
}