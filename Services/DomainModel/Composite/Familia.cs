﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.DomainModel.Composite
{
    /// <summary>
    /// This class (a) defines behaviour for components having children, (b) stores
    /// child components, and (c) implements child-related operations in the Component
    /// interface.
    /// </summary>
    /// 


    public class Familia : Component
    {

        private List<Component> hijos = new List<Component>();

        public string Nombre { get; set; }

        public Familia()
        {

        }


        /// 
        /// <param name="component"></param>
        public override void Add(Component component)
        {

            hijos.Add(component);
        }

        public void AddRange(IEnumerable<Component> components)
        {
            hijos.AddRange(components);
        }

        /// 
        /// <param name="component"></param>
        public override void Remove(Component component)
        {
            component.Remove(component);
        }

        public List<Component> GetHijos()
        {
            return hijos;
        }

    }
}
