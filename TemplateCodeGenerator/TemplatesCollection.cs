using System;
using System.Collections.Generic;

namespace Igloo.Tools
{
    public class TemplateCollection : Dictionary<string, Template>
    {
        private Dictionary<string, Func<string, Template, bool>> _onAddCallbacks = new Dictionary<string, Func<string, Template, bool>>();
        private Dictionary<string, Func<string, bool>> _onIndexerGetCallbacks = new Dictionary<string, Func<string, bool>>();
        private Dictionary<string, Func<string, Template, bool>> _onIndexerSetCallbacks = new Dictionary<string, Func<string, Template, bool>>();

        public void Add(string key, Template template, Func<string, Template, bool> onAdding)
        {
            if (!_onAddCallbacks.ContainsKey(key))
            {
                _onAddCallbacks.Add(key, onAdding);
            }
            else
            {
                _onAddCallbacks[key] = onAdding;
            }

            if (onAdding(key, template))
            {
                base.Add(key, template);
            }
        }

        public new void Add(string key, Template template)
        {
            Func<string, Template, bool> onAdding;

            if (_onAddCallbacks.TryGetValue(key, out onAdding))
            {
                if (onAdding(key, template))
                {
                    base.Add(key, template);
                }
            }
            else
            {
                base.Add(key, template);
            }
        }

        public Template this[string key, Func<string, bool> onIndexerGet, Func<string, Template, bool> onIndexerSet]
        {
            get
            {
                if (!_onIndexerGetCallbacks.ContainsKey(key))
                {
                    _onIndexerGetCallbacks.Add(key, onIndexerGet);
                }
                else
                {
                    _onIndexerGetCallbacks[key] = onIndexerGet;
                }

                if (onIndexerGet(key))
                {
                    return base[key];
                }

                return null;
            }
            set
            {
                if (!_onIndexerSetCallbacks.ContainsKey(key))
                {
                    _onIndexerSetCallbacks.Add(key, onIndexerSet);
                }
                else
                {
                    _onIndexerSetCallbacks[key] = onIndexerSet;
                }

                if (onIndexerSet(key, value))
                {
                    base[key] = value;
                }
            }
        }

        public new Template this[string key]
        {
            get
            {
                Func<string, bool> onIndexerGet;
                if (_onIndexerGetCallbacks.TryGetValue(key, out onIndexerGet))
                {
                    if (onIndexerGet(key))
                    {
                        return base[key];
                    }
                    return null;
                }
                else
                {
                    return base[key];
                }
            }
            set
            {
                Func<string, Template, bool> onIndexerSet;
                if (_onIndexerSetCallbacks.TryGetValue(key, out onIndexerSet))
                {
                    if (onIndexerSet(key, value))
                    {
                        base[key] = value;
                    }
                }
                else
                {
                    base[key] = value;
                }
            }
        }
    }
}
