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
            if i = 19 && ex then
                ans <- true 
                ex <- false
            i <- i + 1
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
    
    member this.Menu =
        let mutable tr = true
        while tr do
            this.IspisiListu
            Console.WriteLine("------------------------------------------")
            Console.WriteLine("Za unos zrakoplova upisite 1")
            Console.WriteLine("Za brisanje zrakoplova upisite 2")
            Console.WriteLine("Za izlazak u glavni menu upisite 3")
            Console.WriteLine("------------------------------------------")
            let mutable unos = Console.ReadLine()
            if unos = "1" then 
                let uzrakoplov = this.UnesiPodatke
                let uspjesno = this.DodajZrakoplov(uzrakoplov)
                if uspjesno then
                    printfn "Zrakoplov dodan"
                else 
                    printfn "Unos nije moguc"
            if unos = "2" then
                printfn "Unesite registraciju"
                let registracija = Console.ReadLine()
                let uspjesno = this.UkloniZrakoplov(registracija)
                if uspjesno then
                    printfn "Zrakoplov izbrisan"
                else
                    printfn "Brisanje nije moguce"
            if unos = "3" then
                tr <- false



[<EntryPoint>]
let main argv = 
    let mutable tr = true
    let mutable brojKoridora = [| for i in 0 .. 2 -> ZracniKoridor("0") |]
    let iz = new ZracniKoridor("iz")
    let sj = new ZracniKoridor("sj")
    let oc = new ZracniKoridor("oc")
    brojKoridora.[0] <- iz
    brojKoridora.[1] <- sj
    brojKoridora.[2] <- oc
    while tr do
        Console.WriteLine("------------------------------------------")
        Console.WriteLine("Zracni koridor broj 1: Istok-Zapad")
        Console.WriteLine("Zracni koridor broj 2" + " Sjever-Jug")
        Console.WriteLine("Zracni koridor broj 3" + " Prekooceanski")
        Console.WriteLine("Unesi broj koridora")
        Console.WriteLine("------------------------------------------")
        
        let izborKoridora = System.Int32.Parse(Console.ReadLine()) - 1
        brojKoridora.[izborKoridora].Menu
        
    0
