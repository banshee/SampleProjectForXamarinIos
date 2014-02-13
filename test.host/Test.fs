namespace com.example

open System
open System.Threading
open System.Threading.Tasks
open NUnit.Framework

// We want a test view that just tracks the
// calls to SetTicks
type TimerViewTestObj() as self = 
    [<DefaultValue>] val mutable ticks: int

    interface ITimerView with
      member x.SetTicks i = self.ticks <- i

[<TestFixture>]
type TimerServiceTest() = 
  [<Test>]
  member self.``demonstrate a failure``() =
    Assert.That(false, Is.True)
    ()

  [<Test>]
  member self.``Sending a tick calls the right method on the view``() =
    let serviceUnderTest = new TimerService()
    let view = new TimerViewTestObj()
    let testThreadContext = Threading.SynchronizationContext.Current
    async {
      do
        TimerCommand.SetTimerView(view, testThreadContext)
        |> serviceUnderTest.Actor.Post

        TimerCommand.Tick
        |> serviceUnderTest.Actor.Post

      // Wait for the test service to finish processing these messages
      let! _ = serviceUnderTest.Actor.PostAndAsyncReply(fun x -> TimerCommand.Syncronize x)

      return ()
    } |> Async.RunSynchronously |> ignore
    Assert.That(view.ticks, Is.EqualTo(1))
    ()