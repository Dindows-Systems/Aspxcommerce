/*
SageFrame® - http://www.sageframe.com
Copyright (c) 2009-2010 by SageFrame
Permission is hereby granted, free of charge, to any person obtaining
a copy of this software and associated documentation files (the
"Software"), to deal in the Software without restriction, including
without limitation the rights to use, copy, modify, merge, publish,
distribute, sublicense, and/or sell copies of the Software, and to
permit persons to whom the Software is furnished to do so, subject to
the following conditions:

The above copyright notice and this permission notice shall be
included in all copies or substantial portions of the Software.

THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND,
EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF
MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND
NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT HOLDERS BE
LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION
OF CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION
WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE.
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Collections;
using SageFrame.ProfileManagement;

namespace SageFrame.Profile
{
    [Serializable()]
    public class ProfilePropertyDefinitionCollection : System.Collections.CollectionBase
    {
        #region "Constructors"

        /// -----------------------------------------------------------------------------
        /// <summary>
        /// Constructs a new default collection
        /// </summary>
        /// <history>
        ///    [alok]    03/25/2010    created
        /// </history>
        /// -----------------------------------------------------------------------------
        public ProfilePropertyDefinitionCollection()
        {
        }

        public ProfilePropertyDefinitionCollection(ArrayList definitionsList)
        {
            AddRange(definitionsList);
        }

        public ProfilePropertyDefinitionCollection(ProfilePropertyDefinitionCollection collection)
        {
            AddRange(collection);
        } 

        #endregion

        #region "Public Properties"

        public sp_ProfileListResult this[int index]
        {
            get
            {
                return ((sp_ProfileListResult)(List[index]));
            }
            set
            {
                List[index] = value;
            }
        }

        public sp_ProfileListResult this[string name]
        {
            get
            {
                return GetByName(name);
            }
        } 

        #endregion

        #region "Public Methods"

        public void AddRange(ArrayList definitionsList)
        {
            foreach (sp_ProfileListResult objProfilePropertyDefinition in definitionsList)
            {
                Add(objProfilePropertyDefinition);
            }
        }

        public void AddRange(ProfilePropertyDefinitionCollection collection)
        {
            foreach (sp_ProfileListResult objProfilePropertyDefinition in collection)
            {
                Add(objProfilePropertyDefinition);
            }
        }

        public int Add(sp_ProfileListResult value)
        {
            return List.Add(value);
        }

        public sp_ProfileListResult GetByName(string name)
        {
            sp_ProfileListResult profileItem = null;
            foreach (sp_ProfileListResult profileProperty in InnerList)
            {
                if ((profileProperty.Name == name))
                {
                    // Found Profile property
                    profileItem = profileProperty;
                }
            }
            return profileItem;
        }

        public bool Contains(sp_ProfileListResult value)
        {
            return List.Contains(value);
        }

        public ProfilePropertyDefinitionCollection GetByCategory(string PropertyTypeName)
        {
            ProfilePropertyDefinitionCollection collection = new ProfilePropertyDefinitionCollection();
            foreach (sp_ProfileListResult profileProperty in InnerList)
            {
                if ((profileProperty.PropertyTypeName == PropertyTypeName))
                {
                    // Found Profile property that satisfies category
                    collection.Add(profileProperty);
                }
            }
            return collection;
        }

        public sp_ProfileListResult GetById(int id)
        {
            sp_ProfileListResult profileItem = null;
            foreach (sp_ProfileListResult profileProperty in InnerList)
            {
                if ((profileProperty.ProfileID == id))
                {
                    // Found Profile property
                    profileItem = profileProperty;
                }
            }
            return profileItem;
        }



        public int IndexOf(sp_ProfileListResult value)
        {
            return List.IndexOf(value);
        }

        public void Insert(int index, sp_ProfileListResult value)
        {
            List.Insert(index, value);
        }

        public void Remove(sp_ProfileListResult value)
        {
            List.Remove(value);
        }

        public void Sort()
        {
            InnerList.Sort(new ProfilePropertyDefinitionComparer());
        } 

        #endregion
    }
}
