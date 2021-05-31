using AutoMapper;
using bison_api.Model;

namespace bison_api.Repository
{
    public class ExampleRep : Interface.IExampleRep
    {
        public bisonContext Context { get; }

        public IMapper Mapper { get; }

        ExampleRep (bisonContext cntx, IMapper mapper)
        {
            Context = cntx;
            Mapper = mapper;
        }
    }
}
