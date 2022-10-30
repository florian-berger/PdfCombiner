using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Linq;

namespace PdfCombiner.Helper
{
    /// <summary>
    ///     An ObservableCollection that supports "AddRange"
    /// </summary>
    /// <typeparam name="T">Typ of the collection</typeparam>
    public class AddRangeObservableCollection<T> : ObservableCollection<T>
    {
        #region Private variables

        private bool _suppressNotification;

        #endregion Private variables

        #region Constructor

        /// <summary>
        ///     Default constructor for an AddRangeObservableCollection.
        /// </summary>
        public AddRangeObservableCollection()
        {
        }

        /// <summary>
        ///     Constructor with initial items AddRangeObservableCollection.
        /// </summary>
        /// <param name="items">List of data</param>
        public AddRangeObservableCollection(IEnumerable<T> items)
        {
            AddRange(items);
        }

        #endregion Constructor

        #region Public methods

        /// <summary>
        ///     Adding an enumeration to the collection
        /// </summary>
        /// <param name="items">Enumerable of new items</param>
        public void AddRange(IEnumerable<T> items)
        {
            if (items == null) throw new ArgumentNullException(nameof(items));

            AddRangeInternal(items);
        }

        /// <summary>
        ///     Adding a list to the collection
        /// </summary>
        /// <param name="items">List of new items</param>
        public void AddRange(List<T> items)
        {
            if (items == null) throw new ArgumentNullException(nameof(items));

            // Nothing changed. Do nothing
            if (items.Count == 0) return;

            AddRangeInternal(items);
        }

        /// <summary>
        ///     Adding an array to the collection
        /// </summary>
        /// <param name="items">Array of new elements</param>
        public void AddRange(params T[] items)
        {
            AddRange(items.ToList());
        }

        /// <summary>
        ///     Sets an enumeration as items of the collection
        /// </summary>
        /// <param name="items">Enumerable that contains the new items</param>
        public void SetRange(IEnumerable<T> items)
        {
            if (items == null)
                throw new ArgumentNullException(nameof(items));

            SetRange(items.ToList());
        }

        /// <summary>
        ///     Sets a list as items of the collection
        /// </summary>
        /// <param name="items">List that contains the new items</param>
        public void SetRange(List<T> items)
        {
            if (items == null)
                throw new ArgumentNullException(nameof(items));

            _suppressNotification = true;

            Clear();
            AddRange(items);
        }

        #endregion Public methods

        #region Protected methods

        /// <summary>
        ///     Event, that will be executed, when the collection items are changed
        /// </summary>
        /// <param name="e">EventArgs</param>
        protected override void OnCollectionChanged(NotifyCollectionChangedEventArgs e)
        {
            if (!_suppressNotification) base.OnCollectionChanged(e);
        }

        #endregion Protected methods

        #region Private methods

        private void AddRangeInternal(IEnumerable<T> items)
        {
            if (items == null) throw new ArgumentNullException(nameof(items));

            _suppressNotification = true;

            foreach (var item in items)
            {
                Add(item);
            }
            _suppressNotification = false;

            OnPropertyChanged(new PropertyChangedEventArgs("Count"));
            OnPropertyChanged(new PropertyChangedEventArgs("Item[]"));

            OnCollectionChanged(new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Reset));
        }

        #endregion Private methods
    }
}
