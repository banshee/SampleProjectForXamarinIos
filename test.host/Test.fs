
namespace test.host
open System
open NUnit.Framework

[<TestFixture>]
type TimerServiceTest() = 
  [<Test>]
  member self.``demonstrate a failure``() =
    Assert.That(false, Is.True)
    ()

  [<Test>]
  member self.``Sending a tick calls the right method on the view``() =
    ()