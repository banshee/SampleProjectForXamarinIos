namespace com.example

#nowarn "40"

open System

type TimerService() =
    let rec waitingForView (mbox : MailboxProcessor<TimerCommand>) : Async<unit> = 
      async {
        let! msg = mbox.Receive()
        let stay = waitingForView mbox
        match msg with
        // XXX rename to include context
        | SetTimerView(v, context) ->
            return! serving mbox v context 0
        | _ -> return! stay
      }
    and serving (mbox : MailboxProcessor<TimerCommand>) (view: ITimerView) context count : Async<unit> =
      async {
        let! msg = mbox.Receive()
        do! Async.SwitchToContext context
        let stay = serving mbox view context count
        match msg with
        | Tick ->
            let newCount = count + 1
            view.SetTicks(newCount)
            return! serving mbox view context newCount
        | SetTimerView(v, c)->
            return! serving mbox v c count
        | ResetTimer ->
            return! serving mbox view context 0
      }
    and actor: MailboxProcessor<TimerCommand> = MailboxProcessor.Start(waitingForView)

    // XXX expose actor and send a message from the view
    member self.Actor = actor

    member self.StartTimer() =
      let timer = new System.Timers.Timer(float 1000)
      timer.AutoReset <- true
      timer.Elapsed.Add (fun x -> printfn "xis %A" x.SignalTime)
      timer.Elapsed.Add (fun x -> Tick |> actor.Post)
      timer.Start()
      timer
