// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ListByValue.cs">
//     Copyright 2016
//           Thomas PIERRAIN (@tpierrain)    
//     Licensed under the Apache License, Version 2.0 (the "License");
//     you may not use this file except in compliance with the License.
//     You may obtain a copy of the License at
//         http://www.apache.org/licenses/LICENSE-2.0
//     Unless required by applicable law or agreed to in writing, software
//     distributed under the License is distributed on an "AS IS" BASIS,
//     WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
//     See the License for the specific language governing permissions and
//     limitations under the License.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------
namespace Value
{
    using System;
    using System.Collections;
    using System.Collections.Generic;

    /// <summary>
    ///     A list with equality based on its content and not on the list's reference 
    ///     (i.e.: 2 different instances containing the same items will be equals).
    /// </summary>
    /// <remarks>This type is not thread-safe (for hashcode updates).</remarks>
    /// <typeparam name="T">Type of the listed items.</typeparam>
    public class ListByValue<T> : EquatableByValue<ListByValue<T>>, IList<T>
    {
        private readonly IList<T> itemsList;

        public ListByValue() : this(new List<T>())
        {
        }

        public ListByValue(IList<T> itemsList)
        {
            this.itemsList = itemsList;
        }

        public IEnumerator<T> GetEnumerator()
        {
            return this.itemsList.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return ((IEnumerable)this.itemsList).GetEnumerator();
        }

        public void Add(T item)
        {
            base.ResetHashCode();
            this.itemsList.Add(item);
        }

        public void Clear()
        {
            base.ResetHashCode();
            this.itemsList.Clear();
        }

        public bool Contains(T item)
        {
            return this.itemsList.Contains(item);
        }

        public void CopyTo(T[] array, int arrayIndex)
        {
            this.itemsList.CopyTo(array, arrayIndex);
        }

        public bool Remove(T item)
        {
            base.ResetHashCode();
            return this.itemsList.Remove(item);
        }

        public int Count { get { return this.itemsList.Count; } }

        public bool IsReadOnly { get { throw new NotImplementedException("Could not expose private IsReadOnly property from aggregated itemsList."); } }

        public int IndexOf(T item)
        {
            return this.itemsList.IndexOf(item);
        }

        public void Insert(int index, T item)
        {
            base.ResetHashCode();
            this.itemsList.Insert(index, item);
        }

        public void RemoveAt(int index)
        {
            base.ResetHashCode();
            this.itemsList.RemoveAt(index);
        }

        public T this[int index]
        {
            get { return this.itemsList[index]; }
            set
            {
                base.ResetHashCode();
                this.itemsList[index] = value;
            }
        }

        protected override IEnumerable<object> GetAllAttributesToBeUsedForEquality()
        {
            return (IEnumerable<object>)this.itemsList;
        }
    }
}