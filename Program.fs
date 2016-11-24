// Learn more about F# at http://fsharp.org
// See the 'F# Tutorial' project for more help.
open System


type Zrakoplov(reg: string, dest: string, vis: int) =
    let mutable Reg = reg
    let mutable Dest = dest
    let mutable Vis = vis

    member this.registracija 
      with get () = Reg
      and set (value) = Reg <- value

    member this.destinacija
      with get () = Dest
      and set (value) = Dest <- value

    member this.visina 
      with get () = Vis
      and set (value) = Vis <- value

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
             i <- 20
          else i <- i + 1
      ans
  
    member this.UkloniZrakoplov(reg: string) =
      let mutable ans = false
      let mutable i = 0
      while i<20 do
          if zrakoplovi.[i].registracija = reg then
              zrakoplovi.[i].registracija <- "0"
              zrakoplovi.[i].destinacija <- "/"
              zrakoplovi.[i].visina <- 0
              i <- 20
              ans <- true
          else i <- i + 1
      ans
    
    member this.IspisiListu =
      let mutable i = 0 
      while i < 20 do
        if not(zrakoplovi.[i].destinacija = "/") then
          Console.WriteLine("------------------------------------------")
          Console.WriteLine("Zrakoplov broj " + (i + 1).ToString())
          Console.WriteLine("Registracija: " + zrakoplovi.[i].registracija)
          Console.WriteLine("Destinacija: " + zrakoplovi.[i].destinacija)
          Console.WriteLine("Visina: " + zrakoplovi.[i].visina.ToString())
          Console.WriteLine("------------------------------------------")
          i <- i + 1
    
    member this.UnesiPodatke = 
            Console.WriteLine("Unesi registraciju zrakoplova")
            let reg = Console.ReadLine()
            Console.WriteLine("Unesi destinaciju zrakoplova")
            let dest = Console.ReadLine()
            Console.WriteLine("Unesi visinu zrakoplova")
            let vis = System.Int32.Parse(Console.ReadLine())
            let zrakoplov = Zrakoplov(reg, dest, vis)
            zrakoplov


[<EntryPoint>]
let main argv = 
    printfn "%A" argv
    0 // return an integer exit code
