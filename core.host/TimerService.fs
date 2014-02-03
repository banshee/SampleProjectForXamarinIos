namespace com.example

#nowarn "40"

open System

type TimerService() =
    let rec waitingForView (mbox : MailboxProcessor<TimerCommand>) : Async<unit> = 
      async {
        let! msg = mbox.Receive()
        let stay = waitingForView mbox
        match msg with
        | SetTimerView v ->
            return! serving mbox v 0
        | _ -> return! stay
      }
    and serving (mbox : MailboxProcessor<TimerCommand>) (view: ITimerView) count : Async<unit> =
      async {
        let! msg = mbox.Receive()
        match msg with
        | Tick ->
            let newCount = count + 1
            view.SetTicks(newCount)
            return! serving mbox view (count + 1)
        | SetTimerView v ->
            return! serving mbox v count
        | ResetTimer ->
            return! serving mbox view 0
      }
    and actor: MailboxProcessor<TimerCommand> = MailboxProcessor.Start(waitingForView)