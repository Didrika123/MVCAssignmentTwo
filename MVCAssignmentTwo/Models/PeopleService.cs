﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MVCAssignmentTwo.Models
{
    public class PeopleService : IPeopleService
    {
        readonly IPeopleRepo _peopleRepo = new InMemoryPeopleRepo();
        public Person Add(CreatePersonViewModel person)
        {
            return _peopleRepo.Create(person.Name, person.PhoneNumber, person.City);
        }

        public PeopleViewModel All()
        {
            return new PeopleViewModel() { Persons = _peopleRepo.Read() };
        }

        public Person Edit(int id, Person person)//What to use id for? Should Person maybe be sort of lacking ID
        {
            return _peopleRepo.Update(person);
        }

        public PeopleViewModel FindBy(PeopleViewModel search)
        {
            //Finding
            string query = search.SearchQuery ?? "";

            if (search.CaseSensitive)
                search.Persons = _peopleRepo.Read().FindAll(n => n.Name.Contains(query) || n.City.Contains(query));
            else
                search.Persons = _peopleRepo.Read().FindAll(n => n.Name.ToLower().Contains(query.ToLower()) || n.City.ToLower().Contains(query.ToLower()));


            //Sorting
            if (search.Sort == PeopleViewModel.SortMode.None)
                search.Persons = search.Persons;
            else if (search.Sort == PeopleViewModel.SortMode.NameAscending)
                search.Persons.Sort((a, b) => a.Name.CompareTo(b.Name));
            else if (search.Sort == PeopleViewModel.SortMode.NameDescending)
                search.Persons.Sort((a, b) => b.Name.CompareTo(a.Name));
            else if (search.Sort == PeopleViewModel.SortMode.CityAscending)
                search.Persons.Sort((a, b) => a.City.CompareTo(b.City));
            else if (search.Sort == PeopleViewModel.SortMode.CityDescending)
                search.Persons.Sort((a, b) => b.City.CompareTo(a.City));


            return search;
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
