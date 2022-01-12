// /*
//    Copyright (c) 2017-12
//
//    moljac
//    Test.cs
//
//    Permission is hereby granted, free of charge, to any person
//    obtaining a copy of this software and associated documentation
//    files (the "Software"), to deal in the Software without
//    restriction, including without limitation the rights to use,
//    copy, modify, merge, publish, distribute, sublicense, and/or sell
//    copies of the Software, and to permit persons to whom the
//    Software is furnished to do so, subject to the following
//    conditions:
//
//    The above copyright notice and this permission notice shall be
//    included in all copies or substantial portions of the Software.
//
//    THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND,
//    EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES
//    OF MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND
//    NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT
//    HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY,
//    WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING
//    FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR
//    OTHER DEALINGS IN THE SOFTWARE.
// */

#if XUNIT
using Xunit;
// NUnit aliases
using Test = Xunit.FactAttribute;
using OneTimeSetUp = HolisticWare.Core.Testing.UnitTests.UnitTestsCompatibilityAliasAttribute;
// XUnit aliases
using TestClass = HolisticWare.Core.Testing.UnitTests.UnitTestsCompatibilityAliasAttribute;
using TestContext = HolisticWare.Core.Testing.UnitTests.TestContext;
#elif NUNIT
using NUnit.Framework;
// MSTest aliases
using TestInitialize = NUnit.Framework.SetUpAttribute;
using TestProperty = NUnit.Framework.PropertyAttribute;
using TestClass = HolisticWare.Core.Testing.UnitTests.UnitTestsCompatibilityAliasAttribute;
using TestMethod = NUnit.Framework.TestAttribute;
using TestCleanup = NUnit.Framework.TearDownAttribute;
// XUnit aliases
using Fact = NUnit.Framework.TestAttribute;
#elif MSTEST
using Microsoft.VisualStudio.TestTools.UnitTesting;
// NUnit aliases
using Test = Microsoft.VisualStudio.TestTools.UnitTesting.TestMethodAttribute;
using OneTimeSetUp = Microsoft.VisualStudio.TestTools.UnitTesting.ClassInitializeAttribute;
// XUnit aliases
using Fact = Microsoft.VisualStudio.TestTools.UnitTesting.TestMethodAttribute;
#endif

#if BENCHMARKDOTNET
using BenchmarkDotNet.Running;
using BenchmarkDotNet.Configs;
using BenchmarkDotNet.Attributes.Jobs;
#else
using Benchmark = HolisticWare.Core.Testing.BenchmarkTests.Benchmark;
using ShortRunJob = HolisticWare.Core.Testing.BenchmarkTests.ShortRunJob;
#endif

using System.Collections.Generic;

using Core;

namespace UnitTests.Core
{
    [TestClass] // for MSTest - NUnit [TestFixture] and XUnit not needed
    public partial class Test_VersionSemantic
    {
        [Test]
        public void Test_VersionSemantic_member_API_01_ctor_01()
        {
            Dictionary<string, VersionSemantic> data;

            data = new Dictionary<string, VersionSemantic>()
            {
                { "1.9.0", new VersionSemantic("1.9.0") },
                { "1.10.0", new VersionSemantic("1.10.0") },
                { "1.11.0", new VersionSemantic() },
                { "1.0.0-alpha", new VersionSemantic() },
                { "1.0.0-alpha.1", new VersionSemantic() },
                { "1.0.0-0.3.7", new VersionSemantic() },
                { "1.0.0-x.7.z.92", new VersionSemantic() },
                { "1.0.0-x-y-z.–", new VersionSemantic() },
                { "1.0.0-alpha+001", new VersionSemantic() },
                { "1.0.0+20130313144700", new VersionSemantic() },
                { "1.0.0-beta+exp.sha.5114f85", new VersionSemantic() },
                { "1.0.0+21AF26D3—-117B344092BD", new VersionSemantic() },
                //{ "", new VersionSemantic() },
                //{ "", new VersionSemantic() },
                //{ "", new VersionSemantic() },
                //{ "", new VersionSemantic() },
                //{ "", new VersionSemantic() },
                //{ "", new VersionSemantic() },
            };

#if MSTEST
            Assert.AreEqual(data["1.9.0"].Major, 1);
#elif NUNIT
            Assert.AreEqual(data["1.9.0"].Major, 1);
#elif XUNIT
            Assert.Equal(data["1.9.0"].Major, 1);
#endif

            return;
        }

        [Test]
        public void Test_VersionSemantic_member_API_01_ctor_02_valid()
        {
            Dictionary<string, VersionSemantic> data;

            data = new Dictionary<string, VersionSemantic>()
            {
                //{ "", new VersionSemantic() },
                { "0.0.4", new VersionSemantic(0, 0, 4) },
                { "1.2.3", new VersionSemantic(1, 2, 3) },
                { "10.20.30", new VersionSemantic("10.20.30") },
                { "1.1.2-prerelease+meta", new VersionSemantic("1.1.2-prerelease+meta") },
                { "1.1.2+meta", new VersionSemantic("1.1.2+meta") },
                { "1.1.2+meta-valid", new VersionSemantic("1.1.2+meta-valid") },
                { "1.0.0-alpha", new VersionSemantic("1.0.0-alpha") },
                { "1.0.0-beta", new VersionSemantic("1.0.0-beta") },
                { "1.0.0-alpha.beta", new VersionSemantic("1.0.0-alpha.beta") },
                { "1.0.0-alpha.beta.1", new VersionSemantic() },
                { "1.0.0-alpha.1", new VersionSemantic("1.0.0-alpha.beta.1") },
                { "1.0.0-alpha0.valid", new VersionSemantic("1.0.0-alpha0.valid") },
                { "1.0.0-alpha.0valid", new VersionSemantic("1.0.0-alpha.0valid") },
                { "1.0.0-alpha-a.b-c-somethinglong+build.1-aef.1-its-okay", new VersionSemantic("1.0.0-alpha-a.b-c-somethinglong+build.1-aef.1-its-okay") },
                { "1.0.0-rc.1+build.1", new VersionSemantic("1.0.0-rc.1+build.1") },
                { "2.0.0-rc.1+build.123", new VersionSemantic() },
                { "1.2.3-beta", new VersionSemantic() },
                { "10.2.3-DEV-SNAPSHOT", new VersionSemantic() },
                { "1.2.3-SNAPSHOT-123", new VersionSemantic() },
                { "1.0.0", new VersionSemantic() },
                { "2.0.0", new VersionSemantic() },
                { "1.1.7", new VersionSemantic() },
                { "2.0.0+build.1848", new VersionSemantic() },
                { "2.0.1-alpha.1227", new VersionSemantic() },
                { "1.0.0-alpha+beta", new VersionSemantic() },
                { "1.2.3----RC-SNAPSHOT.12.9.1--.12+788", new VersionSemantic() },
                { "1.2.3----R-S.12.9.1--.12+meta", new VersionSemantic() },
                { "1.2.3----RC-SNAPSHOT.12.9.1--.12", new VersionSemantic() },
                { "1.0.0+0.build.1-rc.10000aaa-kk-0.1", new VersionSemantic() },
                { "99999999999999999999999.999999999999999999.99999999999999999", new VersionSemantic() },
                { "1.0.0-0A.is.legal", new VersionSemantic() },
            };

            #if MSTEST
            Assert.AreEqual(data["1.9.0"].Major, 1);
            Assert.AreEqual(data["1.9.0"].Minor, 9);
            Assert.AreEqual(data["1.9.0"].Patch, 0);
            Assert.AreEqual(data["1.9.0"].PreRelease, null);
            Assert.AreEqual(data["1.9.0"].Build, null);
#elif NUNIT
            Assert.AreEqual(data["1.9.0"].Major, 1);
            Assert.AreEqual(data["1.9.0"].Minor, 9);
            Assert.AreEqual(data["1.9.0"].Patch, 0);
            Assert.AreEqual(data["1.9.0"].PreRelease, null);
            Assert.AreEqual(data["1.9.0"].Build, null);
#elif XUNIT
            Assert.Equal(data["1.9.0"].Major, 1);
            Assert.Equal(data["1.9.0"].Minor, 9);
            Assert.Equal(data["1.9.0"].Patch, 0);
            Assert.Equal(data["1.9.0"].PreRelease, null);
            Assert.Equal(data["1.9.0"].Build, null);
#endif

            return;
        }

        [Test]
        public void Test_VersionSemantic_member_API_01_ctor_02_invalid()
        {
            Dictionary<string, VersionSemantic> data;

            data = new Dictionary<string, VersionSemantic>()
            {
                //{ "", new VersionSemantic() },
                { "1", null },
                { "1.2", null },
                { "1.2.3-0123", null },
                { "1.2.3-0123.0123", null },
                { "1.1.2+.123", null },
                { "+invalid", null },
                { "-invalid", null },
                { "-invalid+invalid", null },
                { "-invalid.01", null },
                { "alpha", null },
                { "alpha.beta", null },
                { "alpha.beta.1", null },
                { "alpha.1", null },
                { "alpha+beta", null },
                { "alpha_beta", null },
                { "alpha.", null },
                { "alpha..beta", null },
                { "1.0.0-alpha_beta", null },
                { "-alpha.", null },
                { "1.0.0-alpha..1.0.0-alpha..1", null },
                { "1.0.0-alpha...1", null },
                { "1.0.0-alpha....1", null },
                { "1.0.0-alpha.....1", null },
                { "1.0.0-alpha......1", null },
                { "1.0.0-alpha.......1", null },
                { "01.1.1", null },
                { "1.01.1", null },
                { "1.1.01", null },
                { "1.2.3.DEV", null },
                { "1.2-SNAPSHOT", null },
                { "1.2.31.2.3----RC-SNAPSHOT.12.09.1--..12+788", null },
                { "1.2-RC-SNAPSHOT", null },
                { "-1.0.3-gamma+b7718", null },
                { "+justmeta", null },
                { "9.8.7+meta+meta", null },
                { "9.8.7-whatever+meta+meta", null },
                { "99999999999999999999999.999999999999999999.99999999999999999----RC-SNAPSHOT.12.09.1--------------------------------..12", null },
            };

            foreach (KeyValuePair<string, VersionSemantic> kvp in data)
            {
                try
                {
                    VersionSemantic vs = new VersionSemantic(kvp.Key);

                    data[kvp.Key] = vs;
                }
                catch (System.InvalidOperationException exc)
                {
                }
            }

            int a = 2;

            #if MSTEST
            foreach (KeyValuePair<string, VersionSemantic> kvp in data)
            {
                Assert.IsNull(kvp.Value);
            }
            #elif NUNIT
            foreach (KeyValuePair<string, VersionSemantic> kvp in data)
            {
                Assert.IsNull(kvp.Value);
            }
            #elif XUNIT
            foreach (KeyValuePair<string, VersionSemantic> kvp in data)
            {
                Assert.Null(kvp.Value);
            }
            #endif

            return;
        }


        [Test]
        public void Test_VersionSemantic_static_API_01_Parse_02()
        {
            (int major, int minor, int patch, string prerelease, string build) r01;
            (int major, int minor, int patch, string prerelease, string build) r02;
            (int major, int minor, int patch, string prerelease, string build) r03;
            (int major, int minor, int patch, string prerelease, string build) r04;

            r01 = VersionSemantic.ParseDummyNaive("1.0.0");
#if MSTEST
            Assert.AreEqual(r01.major, 1);
            Assert.AreEqual(r01.minor, 0);
            Assert.AreEqual(r01.patch, 0);
            Assert.IsNull(r01.prerelease);
            Assert.IsNull(r01.build);
#elif NUNIT
            Assert.AreEqual(r01.major, 1);
            Assert.AreEqual(r01.minor, 0);
            Assert.AreEqual(r01.patch, 0);
            Assert.IsNull(r01.prerelease);
            Assert.IsNull(r01.build);
#elif XUNIT
            Assert.Equal(r01.major, 1);
            Assert.Equal(r01.minor, 0);
            Assert.Equal(r01.patch, 0);
            Assert.Null(r01.prerelease);
            Assert.Null(r01.build);
#endif

            r02 = VersionSemantic.ParseRegexV01("1.0.0");
            r03 = VersionSemantic.ParseRegexV02("1.0.0");
            // r04 = VersionSemantic.ParseRegexV03("1.0.0");

            r01 = VersionSemantic.ParseDummyNaive("1.0.0-alpha01");
            r02 = VersionSemantic.ParseRegexV01("1.0.0-alpha01");
            r03 = VersionSemantic.ParseRegexV02("1.0.0-alpha01");
            // r04 = VersionSemantic.ParseRegexV03("1.0.0-alpha01");

            r01 = VersionSemantic.ParseDummyNaive("1.0.0-alpha01-20210712");
            r02 = VersionSemantic.ParseRegexV01("1.0.0-alpha01-20210712");
            r03 = VersionSemantic.ParseRegexV02("1.0.0-alpha01-20210712");
            // r04 = VersionSemantic.ParseRegexV03("1.0.0-alpha01-20210712");

            r01 = VersionSemantic.ParseDummyNaive("1.0.0-alpha01+20210712");
            r02 = VersionSemantic.ParseRegexV01("1.0.0-alpha01+20210712");
            r03 = VersionSemantic.ParseRegexV02("1.0.0-alpha01+20210712");
            // r04 = VersionSemantic.ParseRegexV03("1.0.0-alpha01+20210712");

            /*
            r01 = VersionSemantic.ParseDummyNaive("1");
            r02 = VersionSemantic.ParseRegexV01("1");
            r03 = VersionSemantic.ParseRegexV02("1");
            // r04 = VersionSemantic.ParseRegexV03("1");

            r01 = VersionSemantic.ParseDummyNaive("1.0");
            r02 = VersionSemantic.ParseRegexV01("1.0");
            r03 = VersionSemantic.ParseRegexV02("1.0");
            // r04 = VersionSemantic.ParseRegexV03("1.0");
            */


            return;
        }

        [Test]
        public void Test_VersionSemantic_static_API_01_Operator_LessThan_AKA_Precedence_01()
        {
            bool test01 = new VersionSemantic("1.0.0") < new VersionSemantic("2.0.0");
            bool test02 = new VersionSemantic("1.0.0") < new VersionSemantic("2.1.0");
            bool test03 = new VersionSemantic("2.0.0") < new VersionSemantic("2.1.0");
            bool test04 = new VersionSemantic("1.0.0") < new VersionSemantic("2.1.1");
            bool test05 = new VersionSemantic("2.0.0") < new VersionSemantic("2.1.1");
            bool test06 = new VersionSemantic("2.1.0") < new VersionSemantic("2.1.1");


            bool test11 = new VersionSemantic("1.0.0-alpha") < new VersionSemantic("1.0.0-alpha.1");

            bool test12 = new VersionSemantic("1.0.0-alpha") < new VersionSemantic("1.0.0-alpha.beta");
            bool test13 = new VersionSemantic("1.0.0-alpha.1") < new VersionSemantic("1.0.0-alpha.beta");

            bool test21 = new VersionSemantic("1.0.0-alpha") < new VersionSemantic("1.0.0-beta");
            bool test22 = new VersionSemantic("1.0.0-alpha.1") < new VersionSemantic("1.0.0-beta");
            bool test23 = new VersionSemantic("1.0.0-alpha.beta") < new VersionSemantic("1.0.0-beta");

            bool test41 = new VersionSemantic("1.0.0-alpha") < new VersionSemantic("1.0.0-beta.2");
            bool test42 = new VersionSemantic("1.0.0-alpha.1") < new VersionSemantic("1.0.0-beta.2");
            bool test43 = new VersionSemantic("1.0.0-alpha.beta") < new VersionSemantic("1.0.0-beta.2");
            bool test44 = new VersionSemantic("1.0.0-beta") < new VersionSemantic("1.0.0-beta.2");

            bool test51 = new VersionSemantic("1.0.0-alpha") < new VersionSemantic("1.0.0-beta.11");
            bool test52 = new VersionSemantic("1.0.0-alpha.1") < new VersionSemantic("1.0.0-beta.11");
            bool test53 = new VersionSemantic("1.0.0-alpha.beta") < new VersionSemantic("1.0.0-beta.11");
            bool test54 = new VersionSemantic("1.0.0-beta") < new VersionSemantic("1.0.0-beta.11");
            bool test55 = new VersionSemantic("1.0.0-beta.2") < new VersionSemantic("1.0.0-beta.11");

            bool test61 = new VersionSemantic("1.0.0-alpha") < new VersionSemantic("1.0.0-rc.1");
            bool test62 = new VersionSemantic("1.0.0-alpha.1") < new VersionSemantic("1.0.0-rc.1");
            bool test63 = new VersionSemantic("1.0.0-alpha.beta") < new VersionSemantic("1.0.0-rc.1");
            bool test64 = new VersionSemantic("1.0.0-beta") < new VersionSemantic("1.0.0-rc.1");
            bool test65 = new VersionSemantic("1.0.0-beta.2") < new VersionSemantic("1.0.0-rc.1");
            bool test66 = new VersionSemantic("1.0.0-beta.11") < new VersionSemantic("1.0.0-rc.1");

            bool test71 = new VersionSemantic("1.0.0-alpha") < new VersionSemantic("1.0.0-rc");
            bool test72 = new VersionSemantic("1.0.0-alpha.1") < new VersionSemantic("1.0.0-rc");
            bool test73 = new VersionSemantic("1.0.0-alpha.beta") < new VersionSemantic("1.0.0-rc");
            bool test74 = new VersionSemantic("1.0.0-beta") < new VersionSemantic("1.0.0-rc");
            bool test75 = new VersionSemantic("1.0.0-beta.2") < new VersionSemantic("1.0.0-rc");
            bool test76 = new VersionSemantic("1.0.0-beta.11") < new VersionSemantic("1.0.0-rc");
            bool test77 = new VersionSemantic("1.0.0-rc.1") < new VersionSemantic("1.0.0-rc");

            bool test81 = new VersionSemantic("1.0.0-alpha") < new VersionSemantic("1.0.0");
            bool test82 = new VersionSemantic("1.0.0-alpha.1") < new VersionSemantic("1.0.0");
            bool test83 = new VersionSemantic("1.0.0-alpha.beta") < new VersionSemantic("1.0.0");
            bool test84 = new VersionSemantic("1.0.0-beta") < new VersionSemantic("1.0.0");
            bool test85 = new VersionSemantic("1.0.0-beta.2") < new VersionSemantic("1.0.0");
            bool test86 = new VersionSemantic("1.0.0-beta.11") < new VersionSemantic("1.0.0");
            bool test87 = new VersionSemantic("1.0.0-rc.1") < new VersionSemantic("1.0.0");
            bool test88 = new VersionSemantic("1.0.0-rc") < new VersionSemantic("1.0.0");

#if MSTEST
            Assert.AreEqual(test01, true);
#elif NUNIT
            Assert.AreEqual(test01, true);
#elif XUNIT
            Assert. Equal(test01, true);
#endif

            return;
        }

        [Test]
        public void Test_VersionSemantic_static_API_01_Operator_LessThan_AKA_Precedence_02()
        {
            bool test11 = new VersionSemantic("1.0.0-alpha") < new VersionSemantic("1.0.0");
            bool test12 = new VersionSemantic("1.0.0-alpha+20210714") < new VersionSemantic("1.0.0");

            return;
        }
    }
}
