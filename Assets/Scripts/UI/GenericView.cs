using UnityEngine;

namespace UI
{
    public abstract class GenericView<TModel> : MonoBehaviour where TModel : class
    {
        protected TModel Model;

        public void Initialize(TModel model)
        {
            Model = model;
            OnInitialize(model);
        }
        
        protected abstract void OnInitialize(TModel model);

        public virtual void Clear()
        {
            
        }
    }
}