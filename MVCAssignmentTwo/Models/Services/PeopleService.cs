using MVCAssignmentTwo.Models.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MVCAssignmentTwo.Models
{
    public class PeopleService : IPeopleService
    {
        readonly IPeopleRepo _peopleRepo;
        readonly ICitiesService _citiesService;
        public PeopleService(IPeopleRepo peopleRepo, ICitiesService citiesService)
        {
            _peopleRepo = peopleRepo;
            _citiesService = citiesService;
        }
        public Person Add(CreatePersonViewModel person)
        {
            person.City = _citiesService.FindBy(person.City.Id);
            if (person.City == null)
                return null;

            return _peopleRepo.Create(person.Name, person.PhoneNumber, person.City);
        }

        public PeopleViewModel All()
        {
            return new PeopleViewModel() { Persons = _peopleRepo.Read() };
        }
        public PeopleViewModel All(int pageNr)
        {
            return Reduce(new PeopleViewModel() { Persons = _peopleRepo.Read() }, pageNr);
        }


        public Person Edit(int id, CreatePersonViewModel person)
        {
            person.City = _citiesService.LazyFindBy(person.City.Id);
            if (person.City == null)
                return null;

            Person editPerson = new Person(id, person.Name, person.PhoneNumber, person.City);
            return _peopleRepo.Update(editPerson);
        }
        public Person Edit(Person person)
        {
            return _peopleRepo.Update(person);
        }

        public PeopleViewModel FindBy(PeopleViewModel search)
        {
            //Finding
            string query = search.SearchQuery ?? "";

            if (search.CaseSensitive)
                search.Persons = _peopleRepo.Read().FindAll(n => n.Name.Contains(query) || n.City.Name.Contains(query));
            else
                search.Persons = _peopleRepo.Read().FindAll(n => n.Name.ToLower().Contains(query.ToLower()) || n.City.Name.ToLower().Contains(query.ToLower()));


            //Sorting
            if (search.Sort == PeopleViewModel.SortMode.None)
                search.Persons = search.Persons;
            else if (search.Sort == PeopleViewModel.SortMode.NameAscending)
                search.Persons.Sort((a, b) => a.Name.CompareTo(b.Name));
            else if (search.Sort == PeopleViewModel.SortMode.NameDescending)
                search.Persons.Sort((a, b) => b.Name.CompareTo(a.Name));
            else if (search.Sort == PeopleViewModel.SortMode.CityAscending)
                search.Persons.Sort((a, b) => a.City.Name.CompareTo(b.City.Name));
            else if (search.Sort == PeopleViewModel.SortMode.CityDescending)
                search.Persons.Sort((a, b) => b.City.Name.CompareTo(a.City.Name));


            return search;
        }
        public PeopleViewModel FindBy(PeopleViewModel search, int pageNr)
        {
            return Reduce(FindBy(search), pageNr);
        }

        private PeopleViewModel Reduce(PeopleViewModel peopleViewModel, int pageNr)
        {
            int count = peopleViewModel.NumEntriesPerPage; 
            int maxPage = (peopleViewModel.Persons.Count-1) / count;
            if (pageNr > maxPage)
                pageNr = maxPage;

            if (pageNr < 0)
                pageNr = 0;

            peopleViewModel.PageNumber = pageNr;
            int index = pageNr * count;
            if (index + count >= peopleViewModel.Persons.Count)
            {
                count = peopleViewModel.Persons.Count - index; //If exceeding the end, just return all that remains
                peopleViewModel.IsThereMorePages = false;
            }
            else peopleViewModel.IsThereMorePages = true;

            if (index < 0 && index >= peopleViewModel.Persons.Count)
                return null;
            if (count < 0 && count > peopleViewModel.Persons.Count)
                return null;

            if (peopleViewModel.Persons.Count > 0)
            {
                peopleViewModel.FilterString = maxPage == 0 ? "" : $"Page {pageNr + 1} of {maxPage + 1}. ";
                peopleViewModel.FilterString +=  peopleViewModel.Persons.Count < _peopleRepo.Read().Count ? $"Filtered result: {peopleViewModel.Persons.Count} items (of {_peopleRepo.Read().Count})" : "";

                peopleViewModel.Persons = peopleViewModel.Persons.GetRange(index, count);
            }
            else peopleViewModel.FilterString = "No people found.";

            return peopleViewModel;
        }

        public Person FindBy(int id)
        {
            return _peopleRepo.Read(id);
        }

        public bool Remove(int id)
        {
            return _peopleRepo.Delete(_peopleRepo.Read(id));
        }
    }
}
