namespace AttributeService.Application.Cache
{
    public class CacheConstants
    {
        /// <summary>
        /// Attribute Cache 
        /// </summary>
        public const string AttributeCacheKey = "attributeList";
        public const int AttributeCacheTime = 60;

        /// <summary>
        /// AttributeValue Cache 
        /// </summary>
        public const string AttributeValueCacheKey = "attributeValueList";
        public const int AttributeValueCacheTime = 60;

        /// <summary>
        /// Unit Cache 
        /// </summary>
        public const string UnitCacheKey = "unitList";
        public const int UnitCacheTime = 60;
    }
}
