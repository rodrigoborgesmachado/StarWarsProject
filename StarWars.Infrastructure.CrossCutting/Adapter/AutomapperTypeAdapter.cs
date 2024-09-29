using AutoMapper;
using Newtonsoft.Json;
using System.Text;

namespace StarWars.Infrastructure.CrossCutting.Adapter
{
    public class AutomapperTypeAdapter : ITypeAdapter
    {
        #region ITypeAdapter Members

        private readonly IMapper _mapper;

        public AutomapperTypeAdapter()
        {
            _mapper = CustomMapperConfig.Instance.CreateMapper();
        }

        public TTarget Adapt<TSource, TTarget>(TSource source)
            where TSource : class
            where TTarget : class, new()
        {

            var deserializeSettings = new JsonSerializerSettings { ObjectCreationHandling = ObjectCreationHandling.Replace, ReferenceLoopHandling = ReferenceLoopHandling.Ignore };
            var serializedObject = JsonConvert.SerializeObject(source, deserializeSettings);

            return _mapper.Map<TSource, TTarget>(JsonConvert.DeserializeObject<TSource>(serializedObject), JsonConvert.DeserializeObject<TTarget>(serializedObject));
        }

        public TTarget Adapt<TTarget>(object source) where TTarget : class
        {
            JsonSerializer json = new JsonSerializer();

            json.NullValueHandling = NullValueHandling.Ignore;

            json.ObjectCreationHandling = ObjectCreationHandling.Replace;
            json.MissingMemberHandling = MissingMemberHandling.Ignore;
            json.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;


            StringBuilder sb = new StringBuilder();
            using (StringWriter sw = new StringWriter(sb))
            using (JsonTextWriter writer = new JsonTextWriter(sw))
            {
                writer.Formatting = Formatting.None;
                writer.QuoteChar = '"';
                json.Serialize(writer, source);
                writer.Close();
                sw.Close();
            }

            //return _mapper.Map<TTarget>(JsonConvert.DeserializeObject<TTarget>(output))
            return _mapper.Map<TTarget>(JsonConvert.DeserializeObject<TTarget>(sb.ToString()));
        }

        #endregion
    }
}
