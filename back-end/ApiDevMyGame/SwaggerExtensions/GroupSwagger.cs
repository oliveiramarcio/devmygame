using System;

namespace ApiDevMyGame.SwaggerExtensions
{
    /// <summary>
    /// 
    /// </summary>
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false)]
    public class GroupSwagger : Attribute
    {
        /// <summary>
        /// 
        /// </summary>
        public string GroupName { get; private set; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="groupName"></param>
        public GroupSwagger(string groupName)
        {
            if (string.IsNullOrEmpty(groupName))
            {
                throw new ArgumentNullException(groupName);
            }

            GroupName = groupName;
        }
    }
}