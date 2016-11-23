// Learn more about F# at http://fsharp.org
// See the 'F# Tutorial' project for more help.
open System


type Zrakoplov(reg: string, dest: string, vis: int) =
    member this.registracija = reg
    member this.destinacija = dest
    member this.visina = vis

type ZracniKoridor(Ime: string) =
    
    let mutable brojZrakoplova = 0

    let mutable zrakoplovi = [| for i in 0 .. 19 -> Zrakoplov("0", "/", 0)|]

    member this.ime = Ime
    
    member this.checkforSameheight(zrakoplov: Zrakoplov) =
        let mutable ex = true
        let mutable ans = false
        let mutable i = 0
        while ex do
           if not(zrakoplovi.[i].destinacija = "/") && zrakoplovi.[i].visina = zrakoplov.visina then 
              ex <- false
           if i = 20 then
              ans <- true 
              ex <- false
        ans

  
    member this.DodajZrakoplov(zrakoplov: Zrakoplov) =
      let mutable ans = true
      if brojZrakoplova >= 20 then 
        ans <- false
      ans <- this.checkforSameheight(zrakoplov)
      if ans then
        let mutable i = 0
        while i<20 do 
          if zrakoplovi.[i].destinacija = "/" then
             zrakoplovi.[i] <- zrakoplov
             brojZrakoplova <- brojZrakoplova + 1

      ans
  


[<EntryPoint>]
let main argv = 
    printfn "%A" argv
    0 // return an integer exit code
