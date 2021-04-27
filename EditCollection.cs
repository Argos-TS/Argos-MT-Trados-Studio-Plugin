using System.Collections.Generic;

namespace Sdl.Community.ArgosTranslateTradosPlugin
{
    /// <summary>
    /// A collection of <code>EditItem</code> objects.
    /// </summary>
    public class EditCollection
    {
        public List<EditItem> Items { get; set; }

        public EditCollection()
        {
            Items = new List<EditItem>();
        }
    }
}
