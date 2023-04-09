using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows.Data;
using Filer2.Model;

namespace Filer2.ViewModel
{
    class NavigationViewModel : INotifyPropertyChanged
    {
        private CollectionViewSource MenuItemsCollections;
        public ICollectionView SourseCollection => MenuItemsCollections.View;

        public NavigationViewModel()
        {
            ObservableCollection<MenuItems> menuItems = new ObservableCollection<MenuItems>
            {
                new MenuItems { MenuName = "Home", MenuImg = @"Assets\icons\icons_V1.png"},
                new MenuItems {MenuName = "Desctop", MenuImg = @"Assets\icons\icons_V2.png" },
                new MenuItems {MenuName = "Documents", MenuImg = @"Assets\icons\icons_V2.png"},
                new MenuItems {MenuName = "Downloads", MenuImg = @"Assets\icons\icons_V2.png"},
                new MenuItems {MenuName = "Pictures", MenuImg = @"Assets\icons\icons_V2.png"},
                new MenuItems {MenuName = "Music", MenuImg = @"Assets\icons\icons_V2.png"},
                new MenuItems {MenuName = "Movies", MenuImg = @"Assets\icons\icons_V2.png"},
                new MenuItems {MenuName = "Trash", MenuImg = @"Assets\icons\icons_V2.png"}
            };

            MenuItemsCollections = new CollectionViewSource { Source = menuItems };
            MenuItemsCollections.Filter += MenuItems_Filter;
        }

        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanget(string PropertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(PropertyName));
        }

        private string filterText;
        public string FilterText
        {
            get => filterText;
            set
            {
                filterText = value;
                MenuItemsCollections.View.Refresh();
                OnPropertyChanget("FilterText");
            }
        }

        private void MenuItems_Filter(object sender, FilterEventArgs e)
        {
            if(string.IsNullOrEmpty(FilterText))
            {
                e.Accepted = true;
                return;
            }

            MenuItems _items = e.Item as MenuItems;
            if(_items.MenuName.ToUpper().Contains(FilterText.ToUpper()))
            {
                e.Accepted = true;
            }
            else
            {
                e.Accepted = false;
            }
        }
    }
}
