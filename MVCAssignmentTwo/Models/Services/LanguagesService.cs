using MVCAssignmentTwo.Models.Data;
using MVCAssignmentTwo.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MVCAssignmentTwo.Models.Services
{
    public class LanguagesService : ILanguagesService
    {
        readonly ILanguagesRepo _languagesRepo;
        public LanguagesService(ILanguagesRepo languagesRepo)
        {
            _languagesRepo = languagesRepo;
        }
        public Language Add(LanguageViewModel language)
        {
            return _languagesRepo.Create(language.Name, language.Abbreviation);
        }

        public LanguagesViewModel All()
        {
            return new LanguagesViewModel() { Languages = _languagesRepo.Read() };
        }
        public LanguagesViewModel All(int pageNr)
        {
            return new LanguagesViewModel() { Languages = _languagesRepo.Read() };
        }


        public Language Edit(int id, LanguageViewModel language)
        {
            Language editLanguage = new Language(id, language.Name, language.Abbreviation);
            return _languagesRepo.Update(editLanguage);
        }

        public LanguagesViewModel FindBy(LanguagesViewModel search)
        {
            return null;
        }
        public LanguagesViewModel FindBy(LanguagesViewModel search, int pageNr)
        {
            return FindBy(search);
        }
        public Language FindBy(int id)
        {
            return _languagesRepo.Read(id);
        }

        public Language LazyFindBy(int id)
        {
            return _languagesRepo.LazyRead(id);
        }
        public bool Remove(int id)
        {
            return _languagesRepo.Delete(_languagesRepo.Read(id));
        }
    }
}
