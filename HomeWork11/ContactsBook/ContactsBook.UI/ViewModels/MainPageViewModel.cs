using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using ContactsBook.DAL.Data;
using ContactsBook.DAL.Models;
using System.Collections.ObjectModel;

namespace ContactsBook.UI.ViewModels
{
    public partial class MainPageViewModel : ObservableObject
    {
        [ObservableProperty]
        string contactName;

        [ObservableProperty]
        string contactSurname;

        [ObservableProperty]
        string contactPhone;

        ContactItem selectedContact;

        [ObservableProperty]
        ObservableCollection<ContactItem> contactList;

        private readonly AppDbContext _context;

        public MainPageViewModel(AppDbContext context)
        {
            _context = context;
            contactList = new ObservableCollection<ContactItem>();
            LoadData();
        }

        public ContactItem SelectedContact
        {
            get => selectedContact;
            set
            {
                if (selectedContact != value)
                {
                    selectedContact = value;
                    ContactName = selectedContact.Name;
                    ContactSurname = selectedContact.Surname;
                    ContactPhone = selectedContact.Phone;
                    OnPropertyChanged();
                }
            }
        }

        [RelayCommand]
        async void AddContact()
        {
            var contact = new ContactItem
            {
                Name = ContactName,
                Surname = ContactSurname,
                Phone = ContactPhone,
            };
            _context.ContactsList.Add(contact);
            await _context.SaveChangesAsync();          
            LoadData();

            ContactName = string.Empty;
            ContactSurname = string.Empty;
            ContactPhone = string.Empty;
            selectedContact = null;
        }

        [RelayCommand]
        async void DeleteContact()
        {
            if (selectedContact != null)
            {
                _context.ContactsList.Remove(selectedContact);
                await _context.SaveChangesAsync();
                LoadData();
                ContactName = string.Empty;
                ContactSurname = string.Empty;
                ContactPhone = string.Empty;
                selectedContact = null;
            }
        }

        [RelayCommand]
        async void EditContact()
        {
            if (selectedContact != null)
            {
                var contact = _context.ContactsList.Where(item => item.Id == selectedContact.Id).FirstOrDefault();
                contact.Name = ContactName;
                contact.Surname = ContactSurname;
                contact.Phone = ContactPhone;            
                await _context.SaveChangesAsync();
                LoadData();
                ContactName = string.Empty;
                ContactSurname = string.Empty;
                ContactPhone = string.Empty;
                selectedContact = null;
            }
        }

        public void LoadData()
        {
            contactList.Clear();
            foreach (var contact in _context.ContactsList.ToList())
            {
                contactList.Add(contact);
            }
        }
    }
}


