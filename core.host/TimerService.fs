namespace com.example

#nowarn "40"

open System

type TimerService() =
    let rec waitingForView (mbox : MailboxProcessor<TimerCommand>) : Async<unit> = 
      async {
        let! msg = mbox.Receive()
        match msg with
        | Tick(x) ->
            return! waitingForView mbox
        | SetTimerView v ->
            return! serving mbox v
      }
    and serving (mbox : MailboxProcessor<TimerCommand>) (view: ITimerView) : Async<unit> =
      async {
        let! msg = mbox.Receive()
        match msg with
        | Tick(x) ->
            return! waitingForView mbox
        | SetTimerView v ->
            return! serving mbox v
      }
    and actor: MailboxProcessor<TimerCommand> = MailboxProcessor.Start(waitingForView)