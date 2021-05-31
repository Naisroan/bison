using AutoMapper;
using bison_api.Model;

namespace bison_api.Interface
{
    interface IExampleRep
    {
        public bisonContext Context { get; }

        public IMapper Mapper { get; }
    }
}
