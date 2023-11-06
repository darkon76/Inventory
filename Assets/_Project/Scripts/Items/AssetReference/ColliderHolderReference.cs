using System;
using Utils;

namespace Items.AssetReference
{
    [Serializable]
    public class ColliderHolderReference : ComponentReference<CollidersHolder>
    {
        public ColliderHolderReference(string guid) : base(guid)
        {
        }
    }
}