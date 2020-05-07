using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SQLite;
using Xamarin.Forms;

namespace EndProject
{
    public partial class MainPage : ContentPage
    {
        public List<Job> ContactsList;

        public MainPage()
        {
            InitializeComponent();
            update();
        }

        public async void update()
        {
            SQLiteAsyncConnection connection = DependencyService.Get<SQLiteInterface>().GetConnection();

            await connection.CreateTableAsync<Job>();

            Task<List<Job>> contacts = connection.Table<Job>().ToListAsync();

            ContactsList = null;
            ContactsList = contacts.Result;

            ContactsListDisplay.ItemsSource = null;
            ContactsListDisplay.ItemsSource = ContactsList;
        }

        public async void deleteItem(int index)
        {
            SQLiteAsyncConnection connection = DependencyService.Get<SQLiteInterface>().GetConnection();
            await connection.DeleteAsync(new Job(ContactsList[index].Id, ContactsList[index].Name, ContactsList[index].Important, ContactsList[index].DisplayDelete, ContactsList[index].Display, ContactsList[index].Temp));
            update();
        }

        public async void sqliteAdd()
        {
            WeatherAPI api = new WeatherAPI("Perth, AU");
            Debug.WriteLine(api.GetTemp().ToString());

            SQLiteAsyncConnection connection = DependencyService.Get<SQLiteInterface>().GetConnection();
            await connection.InsertAsync(new Job(ContactsList.Count + 1, textInput.Text, false, false, true, api.GetTemp().ToString()));
            update();
        }

        public async void sqliteDelete()
        {
            SQLiteAsyncConnection connection = DependencyService.Get<SQLiteInterface>().GetConnection();
            await connection.DeleteAllAsync<Job>();
            update();
        }

        public async void sqliteUpdate(int indexList, int indexSQL, bool important)
        {
            SQLiteAsyncConnection connection = DependencyService.Get<SQLiteInterface>().GetConnection();

            Job change = new Job()
            {
                Id = Convert.ToInt32(indexSQL),
                Name = ContactsList[indexList].Name,
                Important = important,
                DisplayDelete = false,
                Display = true,
                Temp = ContactsList[indexList].Temp
            };

            try
            {
                await SaveItemAsync(change);
            }
            catch (Exception e)
            {
                Debug.WriteLine(e);
            }

            update();
        }

        public async void sqliteUpdateView(int indexList, int indexSQL, bool delete, bool display)
        {
            SQLiteAsyncConnection connection = DependencyService.Get<SQLiteInterface>().GetConnection();

            Job change = new Job()
            {
                Id = Convert.ToInt32(indexSQL),
                Name = ContactsList[indexList].Name,
                Important = ContactsList[indexList].Important,
                DisplayDelete = delete,
                Display = display,
                Temp = ContactsList[indexList].Temp
            };

            try
            {
                await SaveItemAsync(change);
            }
            catch (Exception e)
            {
                Debug.WriteLine(e);
            }

            update();
        }

        public Task<int> SaveItemAsync(Job contact)
        {
            Debug.WriteLine("Id:" + contact.Id + " Name:" + contact.Name + " Important:" + contact.Important);

            SQLiteAsyncConnection connection = DependencyService.Get<SQLiteInterface>().GetConnection();

            if (contact.Id != 0)
            {
                return connection.UpdateAsync(contact);
            }
            else
            {
                return connection.InsertAsync(contact);
            }
        }

        private void ChangeImportance(object sender, ItemTappedEventArgs e)
        {
            int index = ContactsList.IndexOf((sender as MenuItem).BindingContext as Job);
            if (ContactsList[index].Important == true)
            {
                sqliteUpdate(index, ContactsList[index].Id, false);
            }
            else if (ContactsList[index].Important == false)
            {
                sqliteUpdate(index, ContactsList[index].Id, true);
            }
        }

        private void ChangeImportanceItemTapped(object sender, ItemTappedEventArgs e)
        {
            int index = ContactsList.IndexOf(e.Item as Job);
            if (ContactsList[index].Important == true)
            {
                sqliteUpdate(index, ContactsList[index].Id, false);
            }
            else if (ContactsList[index].Important == false)
            {
                sqliteUpdate(index, ContactsList[index].Id, true);
            }
        }

        private void SaveButton(object sender, ItemTappedEventArgs e)
        {
            update();
        }

        private void AddButton(object sender, ItemTappedEventArgs e)
        {
            sqliteAdd();
        }

        private async void DeleteButton(object sender, ItemTappedEventArgs e)
        {
            bool answer = await DisplayAlert("ALERT", "Would you like to clear list? ", "Yes", "No");

            if (answer == true)
            {
                sqliteDelete();
            }
        }

        private async void DeleteItem(object sender, ItemTappedEventArgs e)
        {
            int index = ContactsList.IndexOf((sender as MenuItem).CommandParameter as Job);

            bool answer = await DisplayAlert("Danger", "Would you like to delete item? ", "Yes", "No");

            if (answer == true)
            {
                deleteItem(index);
            }
        }

        private async void DeleteItemSwipeButton(object sender, ItemTappedEventArgs e)
        {
            int index = ContactsList.IndexOf((sender as Button).CommandParameter as Job);

            bool answer = await DisplayAlert("Danger", "Would you like to delete item? ", "Yes", "No");

            if (answer == true)
            {
                deleteItem(index);
            }
            else
            {
                sqliteUpdateView(index, ContactsList[index].Id, false, true);
            }
        }

        private void OnSwiped(object sender, SwipedEventArgs e)
        {
            int index = ContactsList.IndexOf(e.Parameter as Job);

            Debug.WriteLine("Swiped my lord: Index: " + ContactsList[index].Id);

            if (ContactsList[index].DisplayDelete == false)
            {
                sqliteUpdateView(index, ContactsList[index].Id, true, false);
            }
            else if (ContactsList[index].DisplayDelete == true)
            {
                sqliteUpdateView(index, ContactsList[index].Id, false, true);
            }
        }
    }
}
