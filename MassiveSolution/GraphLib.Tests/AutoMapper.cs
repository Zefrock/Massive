using System;
using System.Text;
using System.Collections.Generic;
using NUnit.Framework;
using GraphLib.AutoMapper;
using AutoMapper;
using GraphLib.DBModel;

namespace GraphLib.Tests.AutoMapper
{
    /// <summary>
    /// Summary description for AutoMapper
    /// </summary>
    [TestFixture]
    public class AutoMapper
    {
        [Test]
        public void ValidateAutoMapperConfiguration()
        {
            GraphMapper.Configure();

            Mapper.Configuration.AssertConfigurationIsValid();
        }
    }
}
