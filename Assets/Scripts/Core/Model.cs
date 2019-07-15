using System;
using System.Collections.Generic;

namespace uichat.core
{
    public abstract class Model
    {
        private HashSet<Action> _listeners = new HashSet<Action>();

        /// [listener] will be invoked when the model changes.
        public void addListener(Action listener)
        {
            _listeners.Add(listener);
        }

        /// [listener] will no longer be invoked when the model changes.
        public void removeListener(Action listener)
        {
            _listeners.Remove(listener);
        }

        /// Should be called only by [Model] when the model has changed.
        protected void notifyListeners()
        {
            foreach (var l in _listeners)
            {
                l.Invoke();
            }
        }
    }
}