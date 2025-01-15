(*  Brij.  HP. QP. (TM)
    Copyright (c) M. P. Trivedi 2016-2025.  All rights reserved. 
    TrivediBldHdr->
    |Trivedi SrcCtrlHdr File.  Copyright (c) 2015-2025 M. P. Trivedi.  All rights reserved.|637901455|test.fs|none|- - - - - - ->* louisa * St.Francis * PIUTE * princeedward * SALTLAKE * Jefferson * craven * <* Obion * CERROGORDO * fayette * LEFLORE * Suffolk * elmore * SweetGrass * <* BLAND * pittsburg * VANDERBURGH * Pittsburg * california * Coffee * PETROLEUM * <* westcarrollparish * ANTRIM * Titus * fortbend * Audrain * STANLEY * losangeles * <* LINCOLNPARISH * PointeCoupeeParish * lee * Nuckolls * CONECUH * hotspring * EDWARDS * <|- - - - - - ->* louisa * St.Francis * PIUTE * princeedward * SALTLAKE * Jefferson * craven * <* Obion * CERROGORDO * fayette * LEFLORE * Suffolk * elmore * SweetGrass * <* BLAND * pittsburg * VANDERBURGH * Pittsburg * california * Coffee * PETROLEUM * <* westcarrollparish * ANTRIM * Titus * fortbend * Audrain * STANLEY * losangeles * <* LINCOLNPARISH * PointeCoupeeParish * lee * Nuckolls * CONECUH * hotspring * EDWARDS * <|

    "src\UI\loggedUIRunner.fs  --platform:x64 --target:exe --out:logdUIRunr.exe -r:bin\Trivedi.Core.dll -r:bin\Trivedi.CoreAux.dll -r:bin\Trivedi.UI.dll -I:C:\Windows\Microsoft.NET\Framework\v4.0.30319"
    "src\UI\loggedUIRunner.fs  --platform:x64 --target:exe --out:load_logdUIRunr.exe -r:bin\Trivedi.Core.dll -r:bin\Trivedi.UI.dll -I:C:\Windows\Microsoft.NET\Framework\v4.0.30319"

  This module contains fns 2 fmt/output the tkLi, incl a condensed
  ver from tk24_Addenda.fs (shd l8r be expanded to incl tkL)

  NOTE: tk24_Addenda contains total 149 itms

  Updated Jan 9th 4 glot
    to filter4Brij + add subMod, output in z_PseudoDV_Categs

   This ver used to output ser Addenda.dat via Bolero
*)

namespace Trivedi

#nowarn "20" "25" "58" "66" "67" "64" "760" "1182" "1558"


module tkPrn =
    open System
    open System.Text.RegularExpressions
    open System.IO
    open System.Text

    type Tk24 = | Tk24 of (string * string * string * string * string * string * string * string)

    printfn "init Module PseudoDV..."

    let bi = new System.Runtime.Serialization.Formatters.Binary.BinaryFormatter()
    let serBA =
        fun x ->
           use mem = new MemoryStream(100)
           bi.Serialize(mem, x)
           mem.Seek(0L, SeekOrigin.Begin) |> ignore        
           mem.ToArray()
    let defEnc:Encoding = Encoding.UTF8
    let b64EncB = fun (bArr : byte[]) -> Convert.ToBase64String(bArr, Base64FormattingOptions.InsertLineBreaks)
    let b64EncB_SingleLine = fun (bArr : byte[]) -> Convert.ToBase64String(bArr, Base64FormattingOptions.None)
    let b64Enc = fun (s:string) -> defEnc.GetBytes(s) |> b64EncB
    let b64Enc_SingleLine = fun (s:string) -> defEnc.GetBytes(s) |> b64EncB_SingleLine
    let b64Dec = fun (s:string) -> Convert.FromBase64String(s)
    let bytes2Str = fun (bArr : byte[]) -> BitConverter.ToString(bArr)
    let str2Bytes = fun (s:string) -> defEnc.GetBytes(s)

    let ser = fun x -> b64EncB (serBA x)

    let currentDate = DateTime.Now
    let defDt = System.DateTime.MinValue
    let now() = DateTime.Now
    let currTicks() = 
        let tk = DateTime.Now.Ticks.ToString()
        Int32.Parse( tk.Substring(0,tk.Length - 9 ))
    let idTicks() = DateTime.Now.Ticks
    let timeStmp = lazy (DateTime.Now)

    type IModMarker = interface end
    type ITblMarker = interface end

    type CustID_Trivedi_AdminTbl() = interface ITblMarker
    type CustID_Trivedi_TaskTbl() = interface ITblMarker

    //  NOTE: tk24_Addenda contains total 149 itms
    type DocUNID = | DocUNID of string
    type TgtVer = | TgtVer of string

    type Option<'T> with
        static member ઓર = fun (opt:option<_>) def -> if opt.IsSome then opt.Value else def

    let getUNID (cls:string) : DocUNID =
        DocUNID(((timeStmp.Force().Ticks - (DateTime(2001, 1, 1)).Ticks).ToString() + "^" + cls))

    type CoreM24 = | CoreM24 of DocUNID * createdOn:DateTime * lastModifiedOn:DateTime * title:string * content:string * tags:string * flag:string with
        override this.ToString() = 
           let (CoreM24(DocUNID(unid), crDt, modDt, tit, cont, tags, flag)) = this
           "CoreM24 Dat obj created on: " + crDt.ToString() + " lastMod on: " + modDt.ToString() + " title:" + tit
             + " content len: " + cont.Length.ToString() + " tags: " + tags.ToString()

    type Mod24 =    | CoreMod of CoreM24
                            | DocVersionModule
                            | DocEditHistoryModule
                            | DocImgModule
                            | DocRelationModule

    type Task24<'t> = | Task24 of mods:Mod24 list * project:string * moduleNm:string  * subMod:string * objective:string * importance:int * urgency:int * parent:string * completed:bool * completedOn:DateTime * tgtVer:TgtVer * docLinks:string * eLvl:int * tblTy:'t with
        override this.ToString() = 
            let (Task24(m, project, moduleNm, subMod, obj, imp, urg, parnt, compl, complOn, TgtVer(tgtVer), docLinks, eLvl, tblTy)) = this
            let (CoreMod(CoreM24(DocUNID(unid), crDt, modDt, tit, cont, tags, flag))) = m.[0]
            //@Dec30Remmed let tblT = (genArg this).ToString()
            "Task24 of |id:" + unid + "|moduleNm:" + moduleNm + "|title:" + tit + "|"

    //A stand-in dataset
    let addenda = [

    Tk24("Brij", "UI", "dzClient", "Grping &c.", """
    
    In the grpingDlg (+ poss other sites) we nd to add a radioBox to choose amongst UpperCase/Lower/Proper/As-Is
    CtxtHelp shld explain that this feat cn be used to unify diverse/unclean data, e.g. "ProjNm/projnm" into one categ instd. of otherwise.  Shd also mention that the _correct_ way to handle data discrepencies is to select/updateAll to correct the spelling.
    
    """, "", "", "12-19-2024");

    Tk24("Brij", "Core", "Server", "Auto-Updating dzClients", """
    
    How do we do this?
    For v1 we cld simply run a chk on login & show dlg: 
      "Updates r avail.  Pls. upd8 yr cli when you get a chance." or 
      "Yr cli *seriously* out-of-date.  Please upd8 now"
    
    The thing about the cli updating itself is full of issues:
     - Once the cli is loaded we can't overwrite the libs coz they're already in use.  
     - If we use a 'shell' to launch the cli _after_ updates the cli will run *inside* the shell; is that an issue? Memory? MainThread issues? 
     - @rsch: How do others do this?
    
    """, "", "", "12-19-2024");
    
    Tk24("Brij", "Core", "Server", "WebHooks + Logging + Analytics etc.", """
    
    @ToDo: Determine which specific events 2 track + allow hooks into.
    
    The correct way to do this wld be to use monadic wrkflow.
    Since the server's already running req.s async, using the asyncMonad wd be overkill/redundant.
    Use something simple, e.g. the ContMonad (even if it's just {handler (id)}}).
    BUT (important) reImpl the monad as a new one, say svrHandlerMonad.
    This allows us 2 modify the bind to add fns to hook/log at will w/o hacking away at anything else.  Also ideal for fiddling/updating.
    
    """, "", "v1", "12-19-2024");
    
    Tk24("Brij", "Core", "Server", "Usr Analytics", """
    
    Note: This is (@TBD) and the webCli can be v2+ (poll users 2 rank features they wish to see)
    
    @ToDo: Determine which specific events 2 track.
    
    dzCli: We track a set of events (udpateable @ will) and, instd of pumping em out, we write to State.
          At close of session we process this into a sort of report (a record actually) and pump2Svr.  ie, all proc is done cliSide.  Which means cliCrash will lead 2 lost info but so what?
          On the svrSide an AdminTbl cohort will collect info for our perusal @ liesure
    webCli: We have v. few events; maybe track certain of em like
            -SwitchVw -Filter -Tag -QUpd -SetRowBackColor
            Also track some usageInfo eg. SessionLen|TotUpdatedRecs|
            This allows us 2 auto-dump into a clientLogTbl for the dev to use (we cld eventually even offer graphs) 
            Devs can, for instance, send emails w/tips on switchVw etc. to promote feature utilization.  Plus prioritize _their_ efforts based on usage.
    
    """, "", "v1", "12-19-2024");
    
    Tk24("Brij", "Core", "Server", "tbdb: cmp.fs runCmd fn", """
    Note: Bien Sur, most of the tbdbs have been caused by compiler monkey-biz
    If priorities allow: poss useful to have handy (is there another sml prod here? a util? Too much on the plate...)...
    <code><pre>
    #if tbdb
        let runCmd() = 
            List.map (fun x ->
                        let (ar1, ar2) = x
                        Cmp ar1 ar2) (["DOTNET_ROOT", "C:\Users\inets\Documents\mike"; 
                                       "PATH", "C:\Users\inets\Documents\mike;%PATH%";
                                       "DOTNET_MULTILEVEL_LOOKUP"; "0";
                                       "C:\Users\inets\Documents\mike\dotnet.exe", "build -restore Trivedi\UITester.fsproj -o uiTester /flp:logfile=__UITester.txt;Verbosity=minimal"] 
    #endif 
    </pre></code>
    """, "", "@Recon src:code_embded_tbdb", "");
    
    Tk24("Brij", "UI", "winCli", "tbdb: UI_Jan18.fs ty windowType", """
    Note: Bien Sur, most of the tbdbs have been caused by compiler monkey-biz
    Perhaps another name/setup? move this snippet _in situ_ 2 hlp w/reconcil. 
    <code><pre>
    #if tbdb_possMBI
    //we p'haps dont nd this ty anymore...
    //error FS0010: Unexpected type application  in member definition
    type windowType<'t when 't :> ITblMarker> = | DesDoc<'t>
                                                | DatDoc<'t>
    #endif //tbdb_possMBI
    </pre></code>
    """, "", "@Recon src:code_embded_tbdb", "");
    
    Tk24("Brij", "UI", "winCli", "tbdb:UI.fs addRange fns", """
    Note: Bien Sur, most of the tbdbs have been caused by compiler monkey-biz
    UI.fs contains this one tbdb 4 addRange (surely there must be more.)
    We know what it was caused by.
    Decide whether it's worthwhile to reImpl and change all calling sites or to let it rest.  Stylistically we must reImpl.
    <code><pre>
    let TSAddRange (ts:ToolStrip) = 
                ts.Items.AddRange  
                    (List.toArray 
    </pre></code>
    """, "", "@Recon src:code_embded_tbdb", "");
    
    Tk24("Brij", "UI", "winCli", "tbdb:UI.fs ty icnPnl", """
    Note: Bien Sur, most of the tbdbs have been caused by compiler monkey-biz
    UI.fs contains this type:
        type icnPnl<'t> (icnNm, tblT:'t, slug, dsk) as icnP =
    This has l8r been addressed BUT was that a workaround?  Distinctly remember a @ToDo saying "reimpl icnPnl".
    Check
    """, "", "@Recon src:code_embded_tbdb", "");
    
    Tk24("Brij", "UI", "winCli", "tbdb:UI.fs pichaak", """
    Note: Bien Sur, most of the tbdbs have been caused by compiler monkey-biz
    In this file's fn ફોરમ_પીચાક, the 
      frmTBar.Items.Add 
      for "Font" and "Fields" is remmed; CHK (tho pro'lly covered, right? Or is this loc no longer in employ? Diff w/Aux)
    NOTE THAT this tbdb also exists in Aux
    """, "", "@Recon src:code_embded_tbdb", "");
    
    
    Tk24("Brij", "Core", "Server", "tbdb:loggedUIRunnerAM.fs", """
    Note: Bien Sur, most of the tbdbs have been caused by compiler monkey-biz
    This file contains type સાદુ_પાન_Jan which runs in slug4clickHandler:
    <code><pre>
        | "DesignDV" -> 
            tibbie "This is the ver using the NEW Masalas; tbdb"
            ...(remmed)
    </pre></code>
    """, "", "@Recon src:code_embded_tbdb", "");
    
    Tk24("Brij", "Core", "Server", "tbdb: loggedUIRunner.fs mockaroo", """
    Note: Bien Sur, most of the tbdbs have been caused by compiler monkey-biz
    
    There exists a toolStripBtn labelld "Mockaroo" to gen/create કલકતી_પાન_Nov8 4 MockarooDV
    
    """, "", "@Recon src:code_embded_tbdb", "");
    
    Tk24("Brij", "Core", "Server", "tbdb:coreAux.fs categByFiscalQtr fns", """
    Note: Bien Sur, most of the tbdbs have been caused by compiler monkey-biz
    This mod contains fns 
    let loadMock() =
    let genDox (inL:(string * '_a) list) =
    And 3 fns to autoCateg by Year, monthNm + FiscalQtr (settable via cPnl)
    This nds 2 be compl + impl for the demo dbs + general usage
    """, "", "@Recon src:code_embded_tbdb", "");
    
    Tk24("Brij", "Core", "Server", "tbdb: coreAux.fs boids scrambled", """
    Note: Bien Sur, most of the tbdbs have been caused by compiler monkey-biz
    The boids nd to be impl scrambled.
    """, "", "@Recon src:code_embded_tbdb", "");
    
    Tk24("Brij", "Core", "Server", "tbdb: core.fs  tryOfL ext", """
    Note: Bien Sur, most of the tbdbs have been caused by compiler monkey-biz
    
    This is a new fn below 'let tryOf f x ='; obv. nd.ed.
    
    <code><pre>
    #if tbdb
        let tryOfL f li = 
            let res = List.map (fun x -> tryOf f x) li
            let errs, results = List.unzip res
            (errs |> flat) , (results |> flat)
    #endif
    </pre></code>
    """, "", "@Recon src:code_embded_tbdb", "");
    
    
    Tk24("Brij", "Core", "Server", "tbdb: Types: brij.fs BrijTy getDefault", """
    Note: Bien Sur, most of the tbdbs have been caused by compiler monkey-biz
    Note also that tho these're redundant; we nd getDefaults for the type (for newTbls etc.)
    
    <code><pre>
      type BrijTy<'t when 't :> ITblMarker>
      ...
    #if tbdb
        static member bld (id:string) (tit:string option) (cont:string option) (tg:string option) = 
            BrijTy([CoreMod(CoreM(DocUNID(id), now(), now(), ઓર  tit "-", બાઇટ |>| ઓર cont "", ઓર tg "", ""))], "", CustID_Trivedi_AdminTbl())
        static member bldSpoo (id:string) (crDt:DateTime )(tit:string option) (cont:string option) (tg:string option) = 
            BrijTy([CoreMod(CoreM(DocUNID(id), crDt, now(), ઓર  tit "-", બાઇટ |>| ઓર cont "", ઓર tg "", ""))], "", CustID_Trivedi_AdminTbl())
    #endif
    </pre></code>
    """, "", "@Recon src:code_embded_tbdb", "");
    
    Tk24("Brij", "Core", "Server", "tbdb: brij.fs tkDatK pickT", """
    
    Note: Bien Sur, most of the tbdbs have been caused by compiler monkey-biz
    
    <code><pre>
      let tkDatK s = 
          ``⍲`` {
        ....
        Note: res0 is Some(Task([CoreMod(CoreM(DocUNID(tick.ToString() + "^Task"), crDt, ...))))
        // This value is not a function and cannot be applied.
        let pickT = 
          res0 
          |> flatten
          |> lim (fun s n -> 
          let (Task(m, project, moduleNm, subMod, objt, imp, urg, parnt, compl, complOn, TgtVer(tgtVer), docLinks, tblTy)) = n
          let (CoreMod(CoreM(DocUNID(unid), crDt, modDt, tit, cont, tags, flag))) = m.[0]
          (Mtpl.AddLi
              [("unid", box unid);("title", box tit);
              ("project",box project);("moduleNm",box moduleNm);
              ("submodule",box subMod); ("objective",box objt); 
              ("importance", box imp);("urgency", box urg );
              ("completedOn", box complOn); ("tgtVer",box tgtVer); 
              ("docLinks",box docLinks);("content",box cont);
              ("tags",box tags);("flag", box flag);
              ("parent", box parnt); ("completed",box compl)]) :: s) [] 
        let newP = retV |> Mtpl.AddOne "tkPics" (box pickT)
        db "<<<<<<<<<<>>>>>>>>>>tkPicks len:%A ty:%A" (pickT.Length.ToString()) (pickT.GetType().ToString())
        let newP = retV
        db (">>>tkDatK:finSt: " + (newP.ToString()))
        return newP  
        }
    </pre></code>
    """, "", "@Recon src:code_embded_tbdb", "");
    
    Tk24("Brij", "UI", "winCli", "tbfo: UIAux.fs genExpr, predConds", """
    (AFAIK This delta poss only in the file: UI_Jan18.fs)
    This file also has a predConds(Map<fldTy, li<strCond>) which is called frm numerous sites. That nds 2 be impl as well
    <code><pre>
        let genExpr<'t when 't :> ITblMarker> (o:'t) fld cond :Expr<'t> =
            let disCombs = (TyOf o) |> getMods |> પેરુ ( fun m -> getDisComb m) |> fmtExprDiscombs
            bldExpr o fld cond disCombs
    </pre></code>
    """, "", "@Recon src:code_embded_tbfo", "");
    
    Tk24("Brij", "UI", "winCli", "tbfo: UIAux.fs kathoHandlerAux", """
    (AFAIK This delta poss only in the file: UIAuxDec20_FrmDz.fs)
    
    <code><pre>
      let kathoHandlerAux =
        fun cmd categ def paanTyp ->
          ...
          let newCats = 
              match cmd with
              | "kathoCalcuttiCategExpando" ->
                  match exSt with
                  | eAll ->
    #if tbfo
                      (getDatAux tblDef fldLi)
                      |> peru (fun ~ ~ ->
                                  let ro:list<_> = r
                                  let isC:bool = unbox r.[(ro.Length) - 2]
                                  match isC with
                                  | true -> Some(unbox r.[(ro.Length) - 1])
                                  | _ -> None ) |> flat
    #endif //tbfo
    </pre></code>
    """, "", "@Recon src:code_embded_tbfo", "");
    
    Tk24("Brij", "UI", "winCli", "tbfo: UIAux.fs કલકતી_પીચાક", """
    The snippet below PLUS l8r in same fn:
      catBtn.Click.AddHandler
      sortBtn.Click.AddHandler
      (fix 2 rem debgHanlr frm) cancelButton.Click.AddHandler
    
    <code><pre>
      let કલકતી_પીચાક =
          fun (ક્વિમામ:option<_>) (d:Form) ->
            ...
    #if tbfo_Dec30
            colBtn.Click.AddHandler(new EventHandler(fun (sender:obj) (e:EventArgs) ->
                let tblFlds =
                    List.filter (fun itm -> 
                                    let (DocFld(t, slg, isInt, nm)) = itm
                                    isInt = false) usrFlds
                    |> lim (fun itm -> let (DocFld(t, slg, isInt, nm)) = itm
                                        nm)
                match (ગપ્પા_પાન (SizeM,Some("Please select the Columns you wish to include"),None , Some(box (tblFlds,fldLi)), None, d, CheckedLBDlg())) with
                | Some x -> CM <- (CalcuttiMasalo(dvNm,tblDef,_,_,_,_,x,_,fltr,categBy,sortBy,_,xSt, rowTips,Ttips, _))
                | _ -> ()))
    #endif //tbfo_Dec30
    </pre></code>
    """, "", "@Recon src:code_embded_tbfo", "");
    
    Tk24("Brij", "Core", "Server", "tbfo: diff UI w/Brij", """
    There are some dupl itms in Brij/UI; nd 2 ensure resolved or there'll be issues w/callStck, here's an example (UIAux)
    <code><pre>
        let openDes =
    </pre></code>
    """, "", "@Recon src:code_embded_tbfo", "");
    
    Tk24("Brij", "UI", "Server", "tbfo: UIAux.fs default CM", """
    
    We nd to impl this + ensure that newDb adds a default.
    [As decided earlier; a la LN but a 'real' dzItm w/cols frm CoreMod]
    ALSO nd 2 ensure the same for other necc dzItms (frm?)
    
    <code><pre>
    type CalcuttiMasaloAux<'t when 't :> ITblMarker>
      ...
    #if tbfo
      static member getDefault() = 
          let tkColHdrs = ["Title";"Objective";"Importance";"Urgency";"TgtVer";"Tags"]
          let tDef = SaadoMasaloAux("Task Tbl", tkFldList(), brijLogo, TaskTbl())
          CalcuttiMasaloAux("Default tkDV", tDef, DesDocInfDeflt(), None, tkColHdrs, 6, tplFLi, None, None, None, None, [], XAll, false, None, Some(pagOptsAux))
    #endif //tbfo
    </pre></code>
    """, "", "@Recon src:code_embded_tbfo", "");
    
    Tk24("Brij", "UI", "winCli", "tbfo: UI.fs cliHandlr", """
    This fn called from var sites; e.g. pgSize change.  Nds to call svr.
    <code><pre>
        let cliHandlr (req:string) (g:DataGridView) =
                //@ToDo: Placeholder; move to comm module l8r
                tibbie "Req recd: " req
    </pre></code>
    """, "", "@Recon src:code_embded_tbfo", "");
    
    Tk24("Brij", "UI", "winCli", "tbfo: UI.fs grid dvLi ", """
    
    This is probably complete; re-check after Reconciliation/diffing phase.
    
    <code><pre>
    //Stateless @tbfo
        let getToolStripDVList =
            let getDropDnItms dvList = List.map (fun dvNm -> getTSButton dvNm "imgNm" (Some ( fun e -> tibbie "getDropDnItms" ))) dvList |> List.toArray
            let getDVsForTbl tblName = ["DataView one"; "DataView two"; "DataView three"] //@tibbie; hardCoded for now
            //toolStrip.items.add dropdownBtn
            let drpDn = new ToolStripDropDown()
            drpDn.Items.AddRange(getDropDnItms (getDVsForTbl "tbl"))
            new ToolStripDropDownButton(Text = "Switch DataView...", DropDown = drpDn, DropDownDirection = ToolStripDropDownDirection.Left, ShowDropDownArrow = true)
    </pre></code>
    """, "", "@Recon src:code_embded_tbfo", "");
    
    Tk24("Brij", "UI", "winCli", "tbfo: UI.fs ફોરમ_પીચાક", """
    
    These are probably complete (twice, eh?) but chk.
    <code><pre>
        let ફોરમ_પીચાક =
            fun (ક્વિમામ:option<_>) (d:Form) ->
    #if tbfo
            //For now, add ability to 
                add/remove flds from FldDef li
                re/order flds
                add validation criteria (in fldDef?)
                add tooltipTxt
                add optional items (e.g. infoBox) to cell list
                override color/font defaults for particular cells
    #endif
    </pre></code>
    """, "", "@Recon src:code_embded_tbfo", "");
    
    Tk24("Brij", "UI", "dzClient", "tbfo: loggedUIRunnerAM.fs winRef", """
    This file (Dec_22/Jan_23; no others) has a skeleton structure for the idea of tracking openWindowHandles in state, see 2nd snippet below
      The 3rd snippet below adds this feature to ctxtMnu etc.
      Note: There exist apparent helperFns too, diff/port wholesale.
    
    Here's the calling site:
    <code><pre>
        type સાદુ_પાન_Jan 
          ...
          let mutable openWins:BrijWin list = []
    </pre></code>
    
    Here's the def/decl etc.:
    <code><pre>
        type BrijWin = | BrijWin of id:DocUNID * tblId:string * docId:string
    
    #if tbfo_Dec30
        //PseudoCode for openWindows logic
        let getOpenHnd = 
            fun s id ->
                new EventHandler(fun (sender:obj) (e:EventArgs) ->
                    match (isOpen tblID s) with
                    | Some w -> switchToChild w
                    | _ -> 
                        //FIRST add & then launch w pId 
                        let winH = BrijWin((getUNID.ToString() + ^pId), tblID, docId)
                        openWins <- (winH :: openWins)
                        match s with
                        | "DataView" -> tibbie ("icn cmd " + s + " for dvID -> " + id + crlf + "launch tbfo")
                        | _ -> openDes id )
        let openHnd = getOpenHnd "DataView" tblID
        icnLbl.DoubleClick.AddHandler(openHnd)
        txtLbl.DoubleClick.AddHandler(openHnd)
        icnP.DoubleClick.AddHandler(openHnd)
        ....
        DesignDVMenuItem.Click.AddHandler(getOpenHnd "DesignView" tblID)
    #endif //tbfo_Dec30
    </pre></code>
    
      Here's the usage to bld ctxtMenu:
    <code><pre>
    #if tbfo_Dec30
                openWins
                |> lifo (fun i itm x y -> 
                             let (brijWin(_, wTy, docId)) = itm
                             let slg = 
                                 match (docId isDz) with
                                 | true -> "&" + i.ToString() + " DesignView: " + wTy.ToString()
                                 | _ -> "&" + i.ToString() + " DataView: " + wTy.ToString()
                             new ToolStripMenuItem(slg, null, 
                                new EventHandler(fun (o:obj) (e:EventArgs) -> switchToChild i), Keys.Control ||| Keys.i)  //i will have to match
                                |> winMenu.DropDownItems.Add) |> ignore
    #endif //tbfo_Dec30
    </pre></code>
    """, "", "@Recon src:code_embded_tbfo", "");
    
    Tk24("Brij", "Core", "Server", "tbfo: CodeServices", """
    Nd to pull this mod outside Core since we've put it on the backBurner
    @chk New entry in EagleEye?
    <code><pre>
    module CodeServices
      ...
        let loadBldInstr doc =
            readFromDoc //nd. ability to modify from ide
            |> instrToState
    
        let genPortable doc =
            getHeader |> getBldInstr |> encInst
            doc
    
        let loadDocLet =
            loadModList
            |> loadComments
            |> loadCompDirs
            doc
    
        let saveDocLet doc =
            stripComments
            |> stripCompDirs
            |> genPortable
            |> doc2openDlg
    </pre></code>
    """, "", "@Recon src:code_embded_tbfo", "");
    
    Tk24("Brij", "Core", "Server", "tbfo: List extns", """
    Need to reconcile (or separate, @tbd) all existing ext.s; there are two which nd tbfo in the src, as shown below
    <code><pre>
            //ચાર_આંગળી_છે_પણ_આ_નઈ
            static member સિવાય_ઉપર_જુઓ = 
                fun x i -> if i = 0 then x.[1..len-1] else (x.[0..l-1] @ x.[l+1 ..eof])
            //લેનકાસ્ટર_નું_પાઈ
            static member કમેન્ટ_જુઓ =
                fun x -> lifo (fun acc y -> let itm = getRnum 0 acc.Len
                                            y, itm @ acc)  x
    </pre></code>
    """, "", "@Recon src:code_embded_tbfo", "");
    
    Tk24("Brij", "Core", "Server", "tbfo: BrijServer.fs+UI.fs કલકતી_પાન_Svr type", """
    g.CellMouseMove.AddHandler (blds ttip) has more than one open issue
    
    NOTE THAT the l8est work on this issue is in ty કલકતી_પાન_Aux in the file UIAux_Dec20_tkDv.fs
    """, "", "@Recon src:code_embded_tbfo", "");
    
    Tk24("Brij", "Core", "Server", "tbfo: BrijServer.fs કલકતી_પાન_Svr type", """
    
    NOTE that the reason "we may not nd this drpDn anymore" is pro'lly ctxt; ie alr covered in tBar no nd in g; but chk
    
    <code><pre>
        type કલકતી_પાન_Svr<'t when 't :> ITblMarker>
          ...
          let tumbaaku =
              printfn "in કલકતી_પાન tumbaaku..."
              match dvTy with
              | DVType.Dt  ->
                ...
                //@tbfo deferred for bldCtrl since we may not nd this drpDn anymore
                //let swDV = new ToolStripDropDownButton(Text = "Switch DataView...", DropDown = (bldCtrl (getDropDnItms (getDVsForTbl "tbl")) None (new ToolStripDropDown())), DropDownDirection = ToolStripDropDownDirection.Left, ShowDropDownArrow = true)
    </pre></code>
    """, "", "@Recon src:code_embded_tbfo", "");
    
    Tk24("Brij", "Core", "Server", "tbfo: Brij.fs choono...handlers", """
    
    Done: kathoHandler
    Left: chunoHandler; supariHandler; lovelyHandler
    
    Also really shd convt the calling fn to use activePattMatching (F#9 returns bool)
    
    Finally, the callingFn catchall match nds 2 be addressed (raise/throw? mt req opt)
    
    Calling fn:
    <code><pre>
        let svrReq = 
            fun cmd categ def ->
                match cmdTy cmd with
                | "chuno" -> chunoHandler cmd categ def (paanTy cmd)
                | "katho" -> kathoHandler cmd categ def (paanTy cmd)
                | "supari" -> supariHandler cmd categ def (paanTy cmd)
                | "lovely" -> lovelyHandler cmd categ def (paanTy cmd)
    </pre></code>
    Handler example:
    <code><pre>
        let chunoHandler = 
            fun cmd categ def paanTy ->
                let (CalcuttiMasalo(...)) = def
                match def.GetType().GenericTypeArguments.[2] with
                | _ -> 
                    tibbie "Brij: chunoHandler: tbfo"
                    def, (getDat tblDef fldLi)
    </pre></code>
    """, "", "@Recon src:code_embded_tbfo", "");
    
    Tk24("Brij", "Core", "Server", "tbfo: Types: Brij.fs openDes", """
    <code><pre>
    module Gullo =
    ...
        let openDes =
            fun (tblTy:'a) ->
                tibbie "Brij.Gullo.openDes -> circ. refs to Aux etc. arising from @MBI"
    #if tbfo
                match dTy with
                | AdminTbl -> 
                    let dFrm = new Form(Text = "DzDV")
                    //dzDV btns refer to state
                    ...
                    dFrm.Show()
                | _ ->
                    //@ToDo 10.10: Modify to make FileNm to be Fully Qualified Title (tbl is internal)
                    //addenda: get frm SaadoMasalo<'t>
                    let tblID = (tblTy.GetType().ToString()).Substring((tblTy.GetType().ToString()).IndexOf(".")+1) 
                    let fil = Path.Combine(@"C:\Users\inets\Documents\mike\bin\", (tblID + ".bdf"))
                    match File.Exists(fil) with
                    | true -> 
                        tibbie "1"
                        let ob = ((File.ReadAllBytes(fil)) |> deSerBA ) //:?> List<BrijTy<'a>>
                        tibbie ("recd: " + (ob.GetType().ToString()))
                        //let outT = lifo (fun s (x:BrijTy<'a>) -> s + "\r\n" + x.ToString()) "" ob
                        //do ગપ્પા_પાન (SizeM,Some("openDes ->"),None , Some(box outT), None, (new Form()), AdminTbl()) |> ignore
                    | _ -> tibbie ("Sorry, no file exists for " + tblID)
    </pre></code>
    """, "", "@Recon src:code_embded_tbfo", "");
    
    Tk24("Brij", "Core", "Server", "tbfo: Types Brij.fs Cit/Tbl type", """
    <code><pre>
    type Cit<'t> = | Cit of dat:Dat * url:string * src:string * tblTy:'t with
        override this.ToString() = "tbfo"
        
    type Cfg<'t when 't :> ITblMarker> = | Cfg of string * tblTy:'t with
        override this.ToString() = "tbfo"
    
    ...later (remmed)
    type Tbl<'t when 't :> ITblMarker> =    | DatDoc of tblTy:'t
                                            | AdDoc of cl:Ad<'t>
                                            | CfgDoc of cf:Cfg<'t> 
                                            | TblDoc of tblDef:TblDef<'t> with
        static member getAll = fun mtpl (ty:Type) -> ...
    </pre></code>
    
    Here's the 1st:
            let (Cit(d, u, src, tblTy)) = this
            "Cit of " + d.ToString() + " with url: " + u + " with src: " + src
    
    For the Cfg; we decided not to use a du for the docx so @DebugComplete:
      - chk 4 calls
      - remove both tys.
    """, "", "@Recon src:code_embded_tbfo", "");
    
    Tk24("gfx", "Server", "System", "Initial Notes", """
    
    These are the initial notes from EagleEye (Dec '23)
    
    <ul><li>Extn pros: intuitive useCase; offline capab.; alwOn
    </li><li>cons: Managed curr.; debg hassle; decide l8r
    </li><li>PoA: v1 impl Canned -&gt; v2 refactor Blazor out -&gt; v3 refactor Dojo in -&gt; v4 refactor 2 Fs -&gt; v5 refactor 2 sqlLite -&gt; v6 customize
    </li><li>pro.ly nds / can use biz (e.g. res98)
    </li><li>unids for treeModel autogen after ea chng to preserve order
    </li><li>top ranked feature: FTsrch (indexing? use extTool (ripgrep?) via pump-in?)
    </li><li>we'll nd ability to addAttachments
    </li><li>1st manually create `A` frm via tbl which'll help in the Flash proceedings
    </li><li>img addrBox shd have chkboxes (w/radio behav) "Embed:loRes|hiRes" unchecked=refLink
    <li>Chk if common cont wrks (done;DOES)</li></ul>
    """, "", " src:EagleEye", "");
    
    Tk24("Hugh", "Server", "System", "Initial Notes", """
    
    These are the initial notes from EagleEye (Dec '23)
    <ul>
        <li><b>Note</b>: Bizzaro takes precedent, same approach bigger mkt</li>
        <li>Drop-in ability 2 add pg</li>
        <li>Nd to allow styling the pg (expose set params)</li>
        <li>Free ver: 10(?) params, paid ver: unlimtd</li>
        <li>all existng webpk usrs can use</li></ul>
    """, "", " src:EagleEye", "");
    
    Tk24("Bizarro", "Server", "System", "Initial Notes", """
    
    These are the initial notes from EagleEye (Dec '23)
    		<ul><li>To induce atomicity:<br>onTheFlyQry >> sortedD >> head</li>
    		<li>Do v nd 2 prov Q 4 par ops?</li>
    		<li>1 gd apprch: `ix-restr2wooman+prem-chand-donate`<br>
    				Note: curr doesn't really wrk; double-roundtrip (2much)
    		</li></ul>
    """, "", " src:EagleEye", "");
    
    Tk24("Lothar", "Server", "System", "Initial Notes", """
    These are the initial notes from EagleEye (Dec '23)
    <br><b>~6m c# devs</b><br>
    
    <ul><li>Curiously v. exceptions</li>
              <li>launch ev 2-4d</li>
        <li>can wrk on arbit/gen incl "I'll/my eagle'll ..."</li>
        <li>store 5ln @ time; lkup pattns 4 flags</li>
        <li>store selInfo; sortD >> take(len-5)</li></ul>
    
    """, "", " src:EagleEye", "");
    
    Tk24("Spidey", "Server", "System", "Initial Notes", """
    These are the initial notes from EagleEye (Dec '23)
    		<ul><li>Launches slowly & suddenly spreads web of binClses</li>
        <li>ComnUse ver can be quite diff (only1?)</li>
        <li>LrgDoc capab can incorp features from other apps (gfix)</li></ul>
    """, "", " src:EagleEye", "");
    
    Tk24("Brij", "Core", "Server", "Winforms contents", """
    
    ***Regardez bien: 
      Often overlooked; many of these mods exist nowhere else.
    
    (From the file hdr) winforms.fs Contains the foll modules:
    
    	GridTester		Ide
    	Ext (bookmarkPainter/addTreeVwZip/bldPSlideTester)
    	Cambattable
    	deck		
    Note: Somewhere in Aug '23 the monkeyBastas removed all above mods; reinserted Nov15 in all locs.
    	perms (combines earlier Base + Red)
    	Bhojpuri (replaces cPnl, rPnl, etc.; incl new tys)
    	jimmy			Tokenizer
    	Folding			FilePanelUpdates
    	Outliner		frmDelta (ty DeltaTracker...)
    	clientInit (UI/UIAux/Brij updates) <- !!contains new Keywds!!
    	Dnd_ops		DnDMonad
    	---
    	wobbly mods: 
    		AuxAddenda
    		wobblyDat
    		wobbly
    		deserBrijDotDat
    	---
    	dndState
    	main
    	
    New reqmt:  Zeep :  user auth/registration/assign API keys.  Is this doable via webhooks/gitEmail?
    
    """, "", " src:gfx_tkLi_Addenda", "");
    
    Tk24("gfx", "Server", "webObj", "Interlude: Parsec", """
    
    Nd a quick 2-way parse from treeModel 2 jsOb to avoid dealing w/JSON etc.
    - HardCoded order (if model changes code will nd updates)
    - Ensure linewraps OK
    - Use widgetRefresh code from one of the gridx examples (basic; whch swaps dataSrc)
    - CliSide only sends (no updates from Svr)
    - So all changes on cliSide get sent as str 2 svr -> parsec -> replInDb
    - EACH update will have to go thru the whole ser seq (in case of disconnects etc.)
    - Next full Refresh or reInitSession gets new svrPayload
    
    """, "", " src:gfx_tkLi_Addenda", "");
    
    Tk24("gfx", "Server", "wasm", "Wasm/Interop Notes/Links", """
    
    [0] Chk this NEW dotnet doc: https://learn.microsoft.com/en-us/aspnet/core/blazor/javascript-interoperability/call-javascript-from-dotnet?view=aspnetcore-8.0
    
    [1] try this <a href='https://stackoverflow.com/questions/75166466/net-7-jsexport-a-task-in-experimental-webassembly-tool'>example</a> of a method returning Task being (auto)translated to a javascript promise
    
      [JSExport]
      public static Task<string> GetDummyStringAsync(){
          return Task.FromResult("Hello world!");
      }
    ...
    exports.MyClass.GetDummyStringAsync().then(result => console.log(result));
    
    [2] ->-> this shd also work (easier poss to use [3])->
    
    Microsoft.JSInterop.DotNetObjectReference<TValue> Create<TValue> (TValue value) where TValue : class;
    
    type treeOb = | treeOb of string
    let myTreeOb = treeOb(dat |> toWobbly)
      [JSExport]
      public static DotNetObjectReference<treeOb> GetTree(){
          return DotNetObjectReference.Create(myTreeOb);
      }
    ...
    
    [3] examples (mostly one-side, the other; but shd work w/exports too)
    SEE ALSO task/promise impl (imports)
    ***c#: https://github.com/SerratedSharp/CSharpWasmRecipes/blob/main/WasmBrowser.Recipes.WasmClient/Examples/JSObjectProxy.cs
    js: https://github.com/SerratedSharp/CSharpWasmRecipes/blob/main/WasmBrowser.Recipes.WasmClient/wwwroot/JSObjectShim.js
    
    </details>
    """, "", " src:gfx_tkLi_Addenda", "");
    
    Tk24("gfx", "Server", "wasm", "Notes/Links Wasm Updates", """
    
    <br><hr><br>
    (0) <mark>Wobbly (4 TreeModel etc.)</mark>
    <br>
    
    - NO json.  period.  Use webObs.  See if we can cast in Fs & populate an exposed/exported var cliSide
    - So the type method nds 2 be Depr & the prev one reinstated
    - Re the Dnd ops we nd 2 either determine specific ops svrSide or parse the whole thing and do a repl with regExp (cd use fparsec; ovkill? pls that wld force us back to json, which we're closing: coz the parser wd nd to be rewritten for ea diff ty of webOb)
    
    <br><hr><br>
    (1) <mark>Resources</mark>
    <br>
    <a href='https://learn.microsoft.com/en-us/dotnet/api/system.resources.resourcemanager?view=net-8.0'>ResourceManager</a>
    For desktop apps, the ResourceManager class retrieves resources from binary resource (.resources) files.  You can also use a ResourceManager object to retrieve resources directly from a .resources file that is not embedded in an assembly, by calling the CreateFileBasedResourceManager method.
    
    - link to dox for .resources -> https://learn.microsoft.com/en-us/dotnet/fundamentals/runtime-libraries/system-resources-resourcereader
    <code><pre>
      rdr.GetResourceData("gfxLogo") (byte[])
    
      let listResources =
        fun resNm -> 
          // Instantiate a standalone .resources file from its filename.
          var rdr = new ResourceReader(resNm) //"Resources1.resources";
          //enum to scan all resources (gd to get type) : 
          let dict = rdr.GetEnumerator()
          while (dict.MoveNext()) do
            printfn ("key:%A val:%A ty:%A (dict.Key) (dict.Value) (dict.Value.GetType())
          rdr.Close()
    
      let getResourceItm =
        fun resNm itmNm -> 
          let rdr = ~currAssembly
          //Retrieve resources by name with ResourceRdr.GetResourceData
          rdr.GetResourceData (itmNm)
    </pre></code>
    <br><hr><br>
    (3) <mark>Byte[]</mark>
    - Already works in getRes (18th.  Adding slice() also works, return same arrLen)
    - Below methods (incl slice) don't.
    
      (b) <mark>MemoryVw (this has util otherwise too, for ex. security)</mark>
    
            Try This ex; it uses ArraySeg instd of MemVw BUT offers two-way testing
            ** Only thing missing in OUR CURR usage is the slice call [SO TRY THAT]->
                'var jsIncomingData = new Uint8Array(inBytes.slice())'
            https://stackoverflow.com/questions/75251127/memoryview-as-a-return-parameter-from-javascript-to-blazor 
            ** but FIRST try adding slice: 'slice makes a new buffer'
              src: https://stackoverflow.com/questions/75609665/what-is-a-memoryview-in-the-net-browser-wasm-runtime
              [link also has ref 2 the implementation of MemoryView]
    <br>
    <hr><br>
            <mark>DotNet: ArraySegment<Byte> ->	JS: MemoryView</mark>
            MemoryView created for an ArraySegment survives after the interop call and is useful for sharing a buffer. Calling dispose() on a MemoryView created for an ArraySegment disposes the proxy and unpins the underlying .NET array. We recommend calling dispose() in a try-finally block for MemoryView.
    <code><pre>
              interface IMemoryView extends IDisposable {
                  /**
                  * https://github.com/dotnet/runtime/blob/main/src/mono/browser/runtime/dotnet.d.ts
                  * copies elements from provided source to the wasm memory.
                  * target has to have the elements of the same type as the underlying C# array.
                  * same as TypedArray.set()
                  */
                  set(source: TypedArray, targetOffset?: number): void;
                  /**
                  * copies elements from wasm memory to provided target.
                  * target has to have the elements of the same type as the underlying C# array.
                  */
                  copyTo(target: TypedArray, sourceOffset?: number): void;
                  /**
                  * same as TypedArray.slice()
                  */
                  slice(start?: number, end?: number): TypedArray;
                  get length(): number;
                  get byteLength(): number;
              }
    </pre></code>
    """, "", " src:gfx_tkLi_Addenda", "");
    
    Tk24("gfx", "Server", "wasm", "Wasm Impl Overview/tkLi", """
      L Wasm - Docker/VM - brijCsvTsv - brijImpExp - gfxImpExp
          x [as of now, accts not necc.]
          x chk functionality
          x Focus on features: missing from gfx/goodToImpl
          - (googKeep/Obsid) @rsch also the issues wikis 4 ea for featureRequests
          "Users don't like change.  They'll only move to your app if is 10x better... (a founder)"
        VMs-Dockr-Vendors
        x Consider wCompnts for encapsulating webSide (React/Vue/Ang)
        - Consider porting existing wid 2 comp 4 walkthru/testImpl
        - Tree (&c) State:
          Since the cli maintains state w/o writing to the store; 
            Every state change -> transmit to svr -> persist
            Cli refresh/reInit -> get frm svr (as normal)
            That's it.  No script back/forth, just manually update the obj
    
    """, "", " src:gfx_tkLi_Addenda", "");
    
    Tk24("gfx", "Server", "Model", "gfx Tree Dnd", """
      - Check that onClick works for lbls, not only expandos (see <a href='https://dojotoolkit.org/reference-guide/1.10/dijit/Tree-examples.html#dijit-tree-examples'>this</a>)
      - Consider allowing users to use richTxt lbls? (see link above >> mt be ugly)
      - Link above also has expandAll/collapseAll snippet [btns alongside HomeBtn?]
    """, "", " src:gfx_tkLi_Addenda", "");
    
    Tk24("gfx", "Server", "rtEd", "rtEd Impl/Notes", """
    
    * Where is the definitive list of rtEd plugins we've decided to keep?
        x LOCATE for porting
        - Some of em have issues w/incompat
        - We nd 2 also add at least one/two custom ones and/or adapt others (CODE, DocLinks, ??)
        - Convert the header drpDn to txt (nonIllustrative) and add custom _Named_ styles w/span; CPnl opts to (1st only 10, l8r unlim) define these.
        - There is also confusion in the icons with at least two btns: ViewCode/InsertCodeBlock
        - rtEd inlining (lastPl/inSitu has tags et al to graft)
            06_13 attempt to patch inlineWidget beauc issues; no debug errs though.
            just create a new widget by reprod the whole thing w/custom
            (probably internal calls failing)
        - rtEd Plugins + 
          <code> (see dojox.editor.plugins.Blockquote) + 
          postRender stuff for docLinks, wids etc. (DEPENDS on: complete docL stuff)
          <a href='https://github.com/dojo/dijit/blob/8ab4cdc2e4bb03d1bca6e76a2a9179dc8c5d846a/_editor/RichText.js#L1641C3-L1641C22'>dijit</a> PostFilterContent 
    
    x DnD icons nd 2 be updated; we're using 80x80px (*chk* against l8st)
      The dojo Dnd <a hgref='https://github.com/dojo/dijit/blob/master/themes/claro/images/dnd.png'>sprite<> is 16x123px
    
    """, "", " src:gfx_tkLi_Addenda", "");
    
    Tk24("gfx", "Server", "", "", """
    
    """, "", " src:gfx_tkLi_Addenda", "");
    
    Tk24("gfx", "Server", "Form", "FrmManager", """
      Note Dec_11_24: Mostly compld but _chk_
    	_Given_
            - We have access to all vars in blding svrSide
            - We can call _specific_ hardCoded validation methds
            - Add/Build to an obj which is the res
            - Order unknown @ callTime, params known/expandable
            - Note that this must be modular (all state incl., svr only has 2 methods: gen() & proc(); no links in-betw)
            - Maximize 4 simplicity
      - Form (cliSide) nds work to populate vals from dat on load 
    
    """, "", " src:gfx_tkLi_Addenda", "");
    
    Tk24("gfx", "Server", "Dlg", "gfxDlg Notes/tks", """
      Note Dec_11_24: Mostly compld but _chk_
    
      - Consider using faux Dlgs (no popup, w/in pg via TPanes)
      - gfxDlg nds new versions for
          - windowed Ps: P, Pr, (x)Pi, (x)Pri (nd ver for longTxt?)
          - Crd (which nds more member funcs to restrict outpt to face/etc.)
          - Tr?  (can we repurpose the rid stuff?)
    
    """, "", " src:gfx_tkLi_Addenda", "");
    
    Tk24("gfx", "Server", "Form", "TagBldr issue", """
    Jun12_24:
      TagBldr test in wwwroot\index.html: the dev\Misc link loads a test page with tgCLoud; issues w/parsing -> chk <a href='https://github.com/dojo/dojox/issues'>this</a> repo issue
    """, "", " src:gfx_tkLi_Addenda", "");
    
    Tk24("Brij", "UI", "BM", "Time Picker ctrl", """
      Note Dec_11_24: This wid exists on the webCli; we nd to add it svrside/dskCli
    Jun10_24:
      - Time picker (no dt, time only) .net <a href='https://learn.microsoft.com/en-us/dotnet/desktop/winforms/controls/how-to-display-time-with-the-datetimepicker-control?view=netframeworkdesktop-4.8'>wid</a>
        
    """, "", " src:gfx_tkLi_Addenda", "");
    
    
    Tk24("gfx", "Server", "Cal", "Cal Impl (v2?)", """
    Jun03_24:
      - Cal
        Parameterize calNames 4 dynamic population in dropDns
        x Look for other/earlier versions of the demo b4 customization
        See mbox 'Google Cal features' in Jun for ideas
      - Allow usrs to specify whether to show options like (for img url 'archive img locally')
    """, "", " src:gfx_tkLi_Addenda", "");
    
    Tk24("Timpa", "System", "general", "Impl/Initial Notes", """
    Jun03_24:
      - Timpa (New product)
        - Combine + consolid8
        - Debug, tree access
        - Isolate styles reqd
        - Lessify Tims + add new gfx.less files
        - Ad-hoc dynamic gen
    """, "", " src:gfx_tkLi_Addenda", "");
    
    Tk24("gfx", "Server", "Tags", "Tag Mngt", """
      Dec_11_24: Some work compl., _chk_
    May16_24:
    - Tags:
      Nd to offer ability to rename/manage tags
      Node in tree labelled 'Tags' (or in CPnl)
      - either with One Page w/Tps for options, or 
      - separate nodeChildrn for ea option, e.g.
        ViewAll: shows TgCloud w/#s [cd offer select + "Show selected"]
        Manage: Rename/ReTag/Remove
        ViewOrphans: Shows all orphans; usr can open/edit/assign
      RELATED: 
      - nd backend logic for rename nd to propagate to dataItms
      - @TBD: Perhaps a gd idea to reduce tree hierarch to tags?
        Might prove to cumbersome, e.g. Mongo wld fall under career/dev/NoSQL
        Leave as-is @ present but keep option open
    
    Tags to mindtree: see <a href='https://ankiweb.net/shared/info/965278890'>ankiweb</a>'s impl.
    
    """, "", " src:gfx_tkLi_Addenda", "");
    
    Tk24("gfx", "Server", "System", "SaveLogic Impl", """
    May_03_24:
    - SaveLogic
      - NOTHING cliSide, all svrSide
      - cliSide save flow is
          gfxTp.autoSave.add(fun _-> this.Updated = true)
          gfxTp.focusLost() -> if this.Updated, svrReq(save)
      - svrSide use a Monad with this added to bind:
        - new hiddenFld in gfxNote: string list docL
        - (nded for D3 map)
             goThru all content >> pick out docLs >> add2DocL fld
    
    """, "", " src:gfx_tkLi_Addenda", "");
    
    Tk24("gfx", "Server", "Form", "rtEd icns", """
    Icons:
      x Extnd treeWid 4 Settings: ea node gets icn from map(icnNm, lbl) [[[djCfg has the listing]]
      - Modify rtEd icons to show blue on hover
      x Color some (start w/dijit corresp) icons in markup
    
    *****Note Dec_11_24: 
      We pro.lly don't nd all the stuff below, kept 4 ref.  We are now going to address this via the rtEd_autoFmt.ing directives; some oddball sit.s (e.g. list creation) might fail BUT the majority of the crlfs shd work.
    <br><hr><br>
    Add xFrm 4 divs: trim title+cont (1st char imp)
      let XFormDivs() =
        trim(title) |> trim(cont) 
        |> lim (fun x -> 
                  discomb
                  )
    
    (Related) Gen report: Add fns to regExProc w/ActivePatts to list details + subSections which're candidates 4 split.
        Regexp fns from wwnn:
            955 let getMatchesForGrpNums
            956 let getMatchesForGrpNms
            957 let rec replSingle
            962 let reg
            1074 let mvReg
            1075 let giReg1
            1076 let giReg2
            1077 let giReg3
        NOTE
          that it's not necc to proc \n to <br> etc.; just opening it in the ed & resaving will fmt <<@ToDo: how to automate this? loc8 fns...>>
          To make things clearer, remove all the BAK files & reduce gfxAddenda.fs to only a new mod (autoopen, contents: the lets w/datLis).  This can be pasted into gfx be4 main.
        x Some work on this completed, see '0610_regexp.fs' in plnk
    
    """, "", " src:gfx_tkLi_Addenda", "");
    
    Tk24("Brij", "Core", "Server", "code_embedded_ToDo Brij.fs Types", """
    type Mod =  | CoreMod of CoreM
                | DocVersionModule
                | DocEditHistoryModule
                | DocImgModule
                | DocRelationModule
    - @ToDo: #2 + #3 poss nd consolid8n
    - @ToDo: DocAttachmntModule exists, nds 2 be added to ty
    
     DocImgModule of string * list<string * string * byte[]>
     let (nm:string, typ:string, contnt:byte[]) = x
     //@ToDo: nd to conv typ to Enum
    
    type DocFld = ...
        member this.getBoxedGenericVal() =
            let (DocFld(fldTy, _, _, _)) = this
            match fldTy with
    	| ...
            | FldNumber | FldRange -> box 0     //@ToDo Range params?
    	| ...
    
    """, "", "src:code_embded_ToDo", "");
    
    Tk24("Brij", "Core", "Server", "code_embedded_ToDo Brij.fs brijLogo", """
    
        let brijLogo = 
            let logo = Bitmap.FromFile("C:/Users/inets/Documents/mike/src/Data/images/brij.png")
    @ToDo: ALL local refs nd 2 be removed; this has to come from Admin tbl
    
    """, "", "src:code_embded_ToDo", "");
    
    Tk24("Brij", "Core", "Server", "code_embedded_ToDo Brij.fs openDes", """
    
    mod Gullo
        let openDes =
            fun (tblTy:'a) ->
                tibbie "Brij.Gullo.openDes -> circ. refs to Aux etc. arising from @MBI"
                match dTy with
                | ...
                | _ ->
                    //@ToDo 10.10: Modify to make FileNm to be Fully Qualified Title (tbl is internal)
                    //addenda: get frm SaadoMasalo<'t>
                    let tblID = (tblTy.GetType().ToString()).Substring((tblTy.GetType().ToString()).IndexOf(".")+1) 
                    let fil = Path.Combine(@"C:\Users\inets\Documents\mike\bin\", (tblID + ".bdf"))
                    match File.Exists(fil) with
    
    """, "", "src:code_embded_ToDo", "");
    
    Tk24("Brij", "Core", "Server", "code_embedded_ToDo Brij.fs pcDat() ", """
    
            //**@DEPR: This dataset is already in tk.dat
            //@ToDo: Chk for completeness
            let pcDat() = 
    
    """, "", "src:code_embded_ToDo", "");
    
    Tk24("Brij", "Core", "Server", "code_embedded_ToDo Types: Brij.fs tabIdx ", """
    
    type કલકતી_પાન_Svr calls this (the fn doesn't exist elsewhere):
        //@ToDo: chk & update to remove tabI param + refs
        let getTSButtonSvr txt imgNm tabI tt optF :ToolStripItem =
            /// no image so altered to txtOnly
    	...
        this fn called by
    	getPgSzDropDn
    	regTBar.Items.Add (getTSButtonSvr "Add Row
    	regTBar.Items.Add (getTSButtonSvr "Delete Row" .... etc....
    
    """, "", "src:code_embded_ToDo", "");
    
    Tk24("Brij", "Core", "Server", "code_embedded_ToDo loggedUIRunr.fs state ", """
    
        [<EntryPoint>]
        [<STAThread>]
        let main ag =
            printfn "main:1"
            let f = (new Form(...
            ...
            !!^ ["wld", (box x)] (f)
      //@ToDo: dzDV btns refer to state from f; nd to update all calls to dsk
    
    """, "", "src:code_embded_ToDo", "");
    
    Tk24("Brij", "Core", "Server", "code_embedded_ToDo Types: loggedUIRunr.fs CSV ", """
    
    type મીઠૂ_પાન<'t when 't :> ITblMarker> (ctorDef:SaadoMasalo<'t>, own, સ્તિતિ) as f =
      ....
      tBar.Items.Add (getTSButton "Import CSV file..." "delImg.jpg" (Some("Auto-Import field definitions and data from a Comma-delimited CSV file")) (Some(new EventHandler (fun sender e -> 
          let linkLi = "see:" + crlf + "https://stackoverflow.com/questions/53956462/get-column-in-haskell-csv-and-infer-the-column-type" + crlf + "https://deephaven.io/blog/2022/02/23/csv-reader/#:~:text=Type%20inferencing%20intends%20to%20map%20a%20column%20of,short%20if%20a%20memory-constrained%20user%20opted%20for%20that%29." + crlf + "https://stackoverflow.com/questions/60421589/ml-net-type-inference-for-csv-loading" + crlf + "https://github.com/Wittline/csv-schema-inference" + crlf + "** https://itnext.io/building-a-schema-inference-data-pipeline-for-large-csv-files-7a45d41ad4df" + crlf + "https://observablehq.com/@d3/d3-autotype" + crlf + "also srch for 'automatic type inference CSV'"
                                (ગપ્પા_પાન (SizeM,Some("CSV Stuff @ToDo"),None , Some(box (linkLi)), None,own, tblDefCSVDlg())) |> ignore  ))))
    
    """, "", "src:code_embded_ToDo", "");
    
    Tk24("Brij", "UI", "General", "code_embedded_ToDo UI.fs icnColor ", """
    
    This fn not used but _shd_ be:
      //@ToDo: nds tinkering, cleaner approach than painting (curr
      let getImgInNewColor = 
        fun (baseImage:Image) (oldColor:Color) (newColor:Color) -> ...
    
    """, "", "src:code_embded_ToDo", "");
    
    Tk24("Brij", "UI", "General", "code_embedded_ToDo UI.fs Ad.imgNm ", """
    
      //@ToDo: update getButton w/param for imgNm
      let getImg nm = 
          let imgContent = (getAdDoc nm).content()
          ...
    
    """, "", "src:code_embded_ToDo", "");
       
    Tk24("Brij", "UI", "General", "code_embedded_ToDo UI.fs state ", """
    
    Note Dec24: this ty already updated BUT the userOverrides may not have been addressed here/elsewhere
    
    module Dlg =
    ....
      //@ToDo: nd to lookup userOverrides, if any
      //       nd to set c,r,o props/tgs
      type ફીલ્ડ_પેનલ (nm, fTy, slg, સુપારી)  as p =
          inherit Panel(Dock = doc "F") ...
    
      let ફોરમ_પીચાક =
          fun (ક્વિમામ:option<_>) (d:Form) ->
          ...
          let p5 = (ફીલ્ડ_પેનલ  ("InfoBox1", FldInfoBox, "Pls note that each of the settings above can be overriden for any particular cell in the Cell Definition Settings (@ToDo)", 2)) :> Panel
    
    """, "", "src:code_embded_ToDo", "");
    
    Tk24("Brij", "UI", "General", "code_embedded_ToDo UI.fs tBar ", """
    
      let ફોરમ_પીચાક =
          fun (ક્વિમામ:option<_>) (d:Form) ->
          ...
          d.Controls.Add(tBar)  //@ToDo: poss nd to remove existing tBar if autoAdded
    
    """, "", "src:code_embded_ToDo", "");
    
    Tk24("Brij", "UI", "General", "code_embedded_ToDo UI.fs handlr ", """
    
    Note Dec24: Poss just a shim to not have to address lib ver conflicts...
      let cliHandlr (req:string) (g:DataGridView) =
        //@ToDo: Placeholder; move to comm module l8r
        tibbie "Req recd: " req   
    
    """, "", "src:code_embded_ToDo", "");
    
    Tk24("Brij", "UI", "General", "code_embedded_ToDo UI.fs cfg ", """
    
    Note Dec24: Poss addressed BUT chk
    
      type કલકતી_પાન<'t when 't :> ITblMarker> (dvTy, own, def:DVDef<'c, 's, 't>, dat) as g =
      ....
      let chuno =
          printfn "in કલકતી_પાન chuno..."
          ////do procDef df g; runK ...
          //@ToDo: Q: Where is this cfg saved? ini? dvDef?  
    
    """, "", "src:code_embded_ToDo", "");
    
    Tk24("Brij", "UI", "Types", "code_embedded_ToDo UIAux.fs types ", """
    
    UIAux.fs: (itms prez in UI.fs excl.)
      //@ToDo: nds SEVERE cleaning up; esply tagging (earlier we were using this to store roSpan etc; defunct now.
      type ફીલ્ડ_પેનલ_Aux (nm, fTy, slg, સુપારી, tabI, cTorFldVal:option<_>)  as p = ....
    
    """, "", "src:code_embded_ToDo", "");
    
    Tk24("Brij", "UI", "Types", "code_embedded_ToDo UIAux.fs defs ", """
    
      let baseTkDatAux = 
          fun (def:SaadoMasaloAux<'a>) fldLi ->
              //@ToDo: curr this is using Saado; nds to be conv to Calc
    
    """, "", "src:code_embded_ToDo", "");
    
    Tk24("Brij", "UI", "Types", "code_embedded_ToDo UIAux.fs types ", """
    
        [Dec_24: var versionx of this exist, incl કલકતી_પાન_Nov]
        [Diff + Consolid8]
    
      type કલકતી_પાન_Aux 
        ...
        let chuno =
          ...
          g.ColumnCount <- visCols
          //-------------------
          //@ToDo: repl w/ categBy
          let categByHardCoded = 2
    
    """, "", "src:code_embded_ToDo", "");
    
    Tk24("Brij", "UI", "Types", "code_embedded_ToDo UIAux.fs fontColr ", """
    
        [Dec_24: pro.lly addressed via separate wid+fldTy def; but chk]
    
      let બનારસી_પીચાક_eoy =
        fun (ક્વિમામ:option<_>) (d:Form) ->
        ....
      let goolkand() =
        ...
        hdrLbl.Click.AddHandler(new EventHandler(fun (sender:obj) (e:EventArgs) ->
          ...
          //@ToDo: font btn also records color change; return tpl?
          let currDefLblFontBtn:Button = (!!~ "Label Font" p1).Value
          let currDefDataFontBtn:Button = (!!~ "Data Font" p2).Value
          let currusrDefForeColorBtn:Button = (!!~ "Text Color" p3).Value
          let currusrDefBackColorBtn:Button = (!!~ "Background Color" p4).Value
    
    """, "", "src:code_embded_ToDo", "");
    
    Tk24("Brij", "UI", "Types", "code_embedded_ToDo UIAux.fs types ", """
    
        [Dec_24: VERIFY that the stuff below nds 2 go; if so remove before unifying vers.
    HOPEFULLY that's not an ironic comment :-) ]
    
    #if tmpRemmed
        //Jan25: remmed? REMMED? no, this has to go.  Delete in a coupla days.
        //@mbi w/generics on ફીલ્ડ_પેનલ_Aux ctor lastParam.
        //  (works on Fable, tested)
        let કેટ_બાય_પીચાક =
            fun (ક્વિમામ:option<_>) (d:Form) ->
            ...
            let getResult() =
                lim (fun s idx ->
                        let cBtg = "Category" + idx.ToString()
                        //@ToDo: Ensure BanarasiFld.Radio saves on chk to this tag
                        let sotg = "Category" + idx.ToString() + "so"
                        let cB = (!!~ cBtg d).Value :?> ComboBox
                        let so = (!!~ sotg d).Value :?> string
    
    
    """, "", "src:code_embded_ToDo", "");
    
    Tk24("Brij", "UI", "Types", "code_embedded_ToDo UIAux.fs types ", """
    
        type કેટ_પેનલ  (fldL:string list, midP:TableLayoutPanel, d) as this =
        ...
        midP.Controls.Add(this)
        midP.SetColumnSpan(this, 3)
        !!^ [("Row" + currRow.ToString()), box (this)] d
        !!^ ["exprRows", box (currRow + 1)] d
        //!!^ [("RowT" + currRow.ToString()), box (currRow.ToTpl())] d
        
        //@ToDo: Currently userCond is a TextBox; nd to autoUpdate it to widget
        //Perhaps extend the FldType panels for these?
        //@ToDo: "oneOf" shd automatically layout multiline txtBox w/tooltip "...one entry per line"
        //@ToDo: For strings nd to add chkbox 4 "ignoreCase"
        ...
    
    """, "", "src:code_embded_ToDo", "");
    
    Tk24("Brij", "UI", "Types", "code_embedded_ToDo UIAux.fs types ", """
    
        type કેટ_પેનલ  (fldL:string list, midP:TableLayoutPanel, d) as this =
        ...
      member this.Render() =
          let inf:TextBox = (!!~ "categTxt" d).Value
          let rs = ((!!~ "exprRows" d).Value)
          match rs = 0 with
          ...
          | _ ->
              //@ToDo: Update to Tpl; lope off last connector in fold
              //       (also needed 4 toExpr())
              inf.Text <- (lifo (fun s r ->
                                  let ro:કેટ_પેનલ = (!!~ ("Row" + r.ToString()) d).Value
                                  s + crlf + ro.toTxt() ) "" [0.. (rs - 1)])
      member this.toTxt() = "By " + qt + catComb.Text + " - " + opComb.Text + qt
    
    """, "", "src:code_embded_ToDo", "");
    
    Tk24("Wwnn", "Core", "General", "code_embedded_ToDo Wwnn.fs scramble ", """
    
        NOTE_Dec_24: F#9 has some new shuffle fns 2; tho' prob.ly not a scramble like we do.  It's best to impl this coz we mt. nd it in the future.
    
        //@ToDo:07.12 -> This nds to be genericized for all arraySizes [3..7] overkill?
        //Note Dec10_24: AFAIK generic fn (scrambled li n) already completed.
        let scrambled (li:string list array) = 
            if (Array.length li) = 5 then scrambled5 else scrambled3
    
    """, "", "src:code_embded_ToDo", "");
    
    Tk24("Wwnn", "Core", "General", "code_embedded_ToDo Wwnn.fs tbfo ", """
    
    tbfo in pgnProc ->
    
    module PgnProc =
      ...
          //in order to generate Li<Moves> to feed to fenGen (verses earlier + mov)
        //@ToDo: AFTER list ready, chk for dupl positions etc., also run numbers for approximation
        let getEaPos = 
            fun toPl gm ->
            //1st strip off the game res
            //regexp for splitting ea move -> (\d{0,3}\.\s)((.*?)\s)((.*?)\s)
            //access via: num: $1 w: $3 b: $5\n (3-tup)
        **note that this exp DOES NOT get half-moves
        we're stripping off the last few anyway; but we want to ENSURE 
            match toPl with
            | wh -> ie, BF to play -> need a full move
                        getTriple >> strip >> fold 2 list of ever-decr moveLi; escape when hit move 5 (settable)
            | _ ->  ie, BF to play as B -> need a half move
                        getTriple >> strip >> strip last el of triple >> fold 2 list of ever-decr moveLi; escape when hit move 5 (settable)
    
    """, "", "src:code_embded_ToDo", "");
    
    Tk24("gfx", "UI", "Format", "code_embedded_ToDo gfx.fs rtEd ", """
    
      rtEd
        NOTE
          that it's not necc to proc \n to <br> etc.; just opening it in the ed & resaving will fmt <<@ToDo: how to automate this?>>
    
    """, "", "src:code_embded_ToDo", "");
    
    Tk24("gfx", "UI", "Format", "code_embedded_ToDo gfx.fs CSV ", """
    
    @ToDo <a href='https://xcellerant.wordpress.com/2015/01/22/gridx-in-xpages-27-exporting-to-excel/'CSV_Export</a><br>
    
    """, "", "src:code_embded_ToDo", "");
    
    Tk24("Brij", "Core", "Server", "code_embedded_ToDo mBox FTsearch ", """
    
    From mbox "Full-Text-Search"
        
    Exc. resource <a href='https://www.mongodb.com/resources/basics/full-text-search'>here</a> goes into fairly deep territory incl binary search index theory.
    
    Main takeaway:
      - MongoDb fullTxt is sufficient
      - In either case we don't nd 2 bld/impl from scratch
      - IF we go via MongoDb idx, still nd to allow usrs the ability to index/rebuild/optimize (size) etc.
      - HOWEVER we don't nd this: our tenants are isolated and we can handle their requirements via a manual srch; no idx necc. (l8r can optimize based on size)
      - We've already blt a full working search system (incl ranking by # of hits in a doc; probably flatMapping on a Map) probably in Java (spx).  This has all the req.s covered/tested.
      - @ToDo: (i) Locate 
               (ii) Port to Fs
    
    """, "", "src:code_embded_ToDo", "");
    
    Tk24("gfx", "Core", "D3", "code_embedded_ToDo mBox D3 ", """
    
    From mbox "D3 notes + tkLi"
    
    This <a href='https://www.d3indepth.com/hierarchies/'>link</a> shows how to use D3 tree methods, rollup etc., & calc values by grouping etc.
    - srch also for 'layout functions'
    
          <a href='https://github.com/ColinEberhardt/d3-wasm-force/blob/master/README.md'>d3-wasm-force</a>: A drop-in replacement for d3-force, using WebAssembly
    
    D3 @ToDo
    - decide on basic grph 2 use
    - nd zooming (see https://d3-graph-gallery.com/interactivity.html)
    - use subset (online ver of Notes?) to play around with
    - add docL to indiv objs (maybe random @ 1st)
    - docL to links/lines/connected
    
    """, "", "src:code_embded_ToDo", "");
    
    Tk24("gfx", "Core", "D3", "code_embedded_ToDo tree state ", """
    
    From "tree_State.html"
    
    [Verify completed/no longer necc]
    (i)  @ToDo: address this (Compl 5.28; see code)
            state 4 DnD ops/rearranging nodes: 
    		problem with this particular model/tree: 
    		move to diff order within same branch doesn't take (node disappears!)
    
    (ii) state 4 expanded:
    		Will work *with* or *without* lang.extend (just set persist)
    https://github.com/dojo/dijit/blob/8ab4cdc2e4bb03d1bca6e76a2a9179dc8c5d846a/Tree.js#L1692
    		_state: function(node, expanded){
    			// summary:
    			//		Query or set expanded state for an node
    
    also
    		_saveExpandedNodes: function(){
    				var ary = [];
    				for(var id in this._openedNodes){
    					ary.push(id);
    				}
    &		_initState: function(oreo){
    			// summary:
    			//		Load in which nodes should be opened automatically (from cookie persist)
    					array.forEach(oreo.split(','), function(item){
    						this._openedNodes[item] = true;
    					}, this);
    
    
    """, "", "src:code_embded_ToDo", "");
    
    Tk24("gfx", "CSS", "Icons", "code_embedded_ToDo css icons ", """
    
    editorIcons.css 
    .dijitDisabled .dijitEditorIcon {
    	background-image: url('images/editorIconsDisabled.png'); 
    /* editor icons sprite image for the disabled state 
       @ToDo: CHECK: do we need these?  
       Let's leave off right now, this'll show the old ones; then decide...
    */ }
    
    """, "", "src:code_embded_ToDo", "");
    
    Tk24("Brij", "Core", "Server", "code_embedded_ToDo notes.txt var ", """
    
    Note_Dec_24: These're from plnk notes.txt 
    	     - Address them AFTER reconciliatn.
    	     - Cross em off
     	     - Ensure they don't exist elsewhere
    
    CheckedLBDlg ->  Some( box (ચેક્ડ_પીચાક  ક્વિમામ d))
    frmSetupDlg ->  Some( box (બનારસી_પીચાક  ક્વિમામ d))
    tblDefCSVDlg ->  Some( box (સી_એસ_વી_પીચાક  ક્વિમામ d))
    tblDefSetup ->  Some( box (મીઠૂ_પીચાક  ક્વિમામ d))
    dvDefSetupDlg ->  Some( box (કલકતી_પીચાક  ક્વિમામ d))
    dvCatByDlg ->  Some( box (કેટ_બાય_પીચાક  ક્વિમામ d))
    runGDlg  ->  Some( box (ગુરખા_પીચાક  ક્વિમામ d))
    
    
    = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = 
        Nov02
    
        type કલકતી_પાન_Nov<'t when 't :> ITblMarker> (dvTy, own, def:DVDef<'c, 's, 't>, dat) as g =
        
        let musicDV = DVDef("MusicDV", TblDef2("testAdTable", AdminTbl()), docInf, None, musicColHdrs, 4,  ["fld1";"fld2";"fld3";"fld4";"fld5";"fld6"],  None,  None, None, None, [], true, None, Some(pagOpts))
        
        let testItm1 = new ToolStripMenuItem("MusicDV")
        testItm1.Click.AddHandler(new EventHandler(fun (sender:obj) (e:EventArgs) ->
                     let mFrm = new Form(Text = "MusicDV")
                     let musicF = કલકતી_પાન_Nov(Reg, mFrm, musicDV, musicDat)
                     mFrm.Show()))
    
    recreate Nov 28 has MusicDV + Form (poss last b4 refactor)
    baseTkDatAux tDef tkFldLiLocalAux
                     File.WriteAllBytes("baseTkDatAux2.bdf", (serBA newDt))
    = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = 
    @ToDo ~ Nov27->
    
    CalcDef:    nds frmNm assoc w/each + ability to set/chng
                nds CalcF (besides Lookup) for Calcs
                these become hiddenCols or true/false
                (has "keywd") | (fldNum > 789)
                    -> CatBy
                these don't (?@tbd) exist in BanarasiDef
                MeethooDef-> _Y_ BUT nonEditable (use lbl ctrls)
               
    DzDV:       1st DV is deflt (?@tbd); ability to set in CalcDef: checkBoxes
                new TSBtn "Create Copy" for dzDox
    
    Banarasi:   Load hardCdd itms to wld -> TTbl frmTest via Aux
                Test via pickT
    Meethoo:    Complete leftover itms, that gives us a complete-ish set
    = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = 
    Existing mods: continue as b4
    ALL new mods:  keep CoreAux open; use new conventions
    
    @tbd The api will be CRU ('d's become 'u's)
    = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = 
    @ToDo ~ Nov9
    
    mod AutoOp Brij.Canned <- all suppliers; f 'a x -> x
    
    FrmDz:  Layout Logic
            InfoBox etc. (remaining wids)
            Cleanup
            Validation
            PredBldr
            Pullout LV 2 cls
    TblDz:  new() frm abv
    DvDz:   Boyz; use ArrowPnl [] -> [] with upDn btns
            Chooser
            AutoSetup
    LookupDocs: Nd frm/Dlg 2 UI
                Basically impl couple in mock scenarios to determine path
    
    = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = 
    
    Core:
        * Nd a new tpl: sTpl: not a list? for dat HashMap entVs
            @ToTEST: can we just get by w/box?
            IF we nd new one for just the types, EXTEND?
    
        From 6/5->
            * modify st.Bind to call >!>
            * ? {let x = get; do! ? {...}  Q: can we avoid using x (bind takes f not fx?)
    
    Idris 4 utils? <<Compiles 2 C>>
    
    Aux:
        * Do amish 4 3 lists; 1st all 8 & 2nd half-ish  if (1) elif (2) elif (3)...
        * NO existing replaced 4 now
        * After in place mt be a good time to clean mods & 
          move new Core fns in since we now have a working dll set.
    
    Dat:
    * Continue w/adding updated dat fns until all consumed
    
    Brij.Expr:
        * The only place we act'ly nd dyComp is after types change in ???
        * The rest can/shd be handled via exprShape
        * @ToDo: test embedding exprs within others (shd work)
        * @ToDo: find best way to store these
        * @ToDo: determine whether a sep expr is nded for crit.
    
    UI:
    	Note: module Form uses Task etc. types from Brij; consider moving entire mod to a separate fork
        * recr ty / basic frame; add pointLess, fix w/Dirs, no comb
        * new tag/tpl/st single runC in ea Mod
        * sketch out basic data + popDat
        * bur p x y shd rem matches (?)
        * tst: push 3 params + Comb 2 tags; put thro + brush
          simulate via map/list/whatever
          if comp compl use cq |> eval
        * ONCE above verified -> basic 5 (3param) + add'l 4(or as nded) -> Commando mode
    
    """, "", "src:code_embded_ToDo", "");
    
    Tk24("Brij", "Core", "Server", "All handlers", """
    We're currently using paanTy+cmd to handle matches; poss b8r approach to use combined PhantomTys (e.g. paanTyCmd()) since all extant types are known @ compileTime + mbi:hkt.s
    """, "", "", "12-10-2024");
    
    Tk24("ccFam", "Demo", "general", "Var fixes/upd8s", """
      x Update ccF prop to add instr
        https://brijuser2.github.io/ccFam/assets/SysAdminPortal.html
        TP: In Viol
          add "Please click on the screenShot below to see details of the violating Facility" above the link to the img -
          https://github.com/brijUser2/brijuser2.github.io/blob/main/ccFam/assets/redDV.png?raw=true
      x Fix link toc 2 above
      x Add hlpTxt abv 1st TP saying click anywhere on img below to view Roster
    """, "11-26-2024", "", "11-15-2024");
    
    Tk24("Brij", "Core", "Logs", "Diagnostic Logs", """
        ((Based on an idea from karmakaze in HN Thread: <b>"It's not stupid if it works"</b>))  <br>
        That dev (trading app) took an ascii scrnsht of the desktop by walking the tree of Swing els; now if users complain of fill price he asks 'em to do Menu|ViewLog; the price fld is logged...<br>
        Collect + Maintain beauc. State data every session (cliSide) incl <br>
          - Open Tbls<br>
          - Actions (i.e., UIevents)<br>
          - incl timeStamps<br>
        Then, optional storage: onError sendToSvr else discard...<br>
        Or send everything (hey, space is cheap) and recycle when > Xgb
    """, "", " src:gfx_tkLi_Addenda", "");
    
    Tk24("Brij", "Core", "Server", "BM: categBy List Fld", """
    Cover the eventuality of usr choosing a list fld 4 grpBy (show the itm in both?If so, what happens to the totals?  WILL ND BYPASS)
    """, "", "", "12-07-2024");
    
    Tk24("gfx", "System", "general", "In-Mem", """
    gfx can go InMem w/edits + btn 'downloadDeltas'""", "", "cr8d:Dec24", "");
    
    Tk24("Brij", "Other", "Marketing", "Top 100 reasons", """
    ready 2 begin anytime...""", "", "", "11-25-2024");
    
    Tk24("Brij", "UI", "WebClient", "CSS gradients", """
    Nix all gradients, label output files as such (both css/less)
      Nds extra caution w/plugins + dojox
    """, "", "cr8d:Dec24", "");
    
    Tk24("Brij", "Other", "Marketing", "Ebay/Rbay Demo ", """
    The CardVw has a HUGE wow factor [CpType -> Tree|Crd]
      Dz + autoDefault.  Make it so.""", "", "cr8d:Dec24", "");
    
    Tk24("Brij", "UI", "WebClient", "goog Lit", """
    Test 1st w/Cal or other wid THEN grid
      To prevent lock-in and non-reliance look @ output & determine if we can 
      'templatize' the creation.
      We DO NEED to use this, however, in order to ensure compat/standardiztn.""", "", "cr8d:Dec24", "");
    
    Tk24("Brij", "Core", "Server", "Persist: Revert content fldFmt", """
    We nd 2 revert the content fld to txt
    In order to enable mongo-Side FTIdx.  Just run a map fn on the dat.  [Poss alr exists as part of Ttip bldr: ReImpl this`]
        @TBD: Revert in BrijBaseTy.s?""", "", "cr8d:Dec24", "");
    
    Tk24("Brij", "Core", "Server", "Handlers: OpenTbls param", """
    All reqs from dskCli append a new param OpenTbls so we know (no double-sending dzDox etc.)
        AFAIK not necc on webCli (@chk)""", "", "cr8d:Dec24", "");
    
    Tk24("Brij", "Core", "Server", "ExprBldr Updates", """
    We nd to update it thus: instd of returning a LinqExp, it shd return a tuple: (LinqExp, jsScriptExp).  See mBox 'Compiling Expr to JavaScript' for options; or just manually code the js side stuff using snippets from the fltrBldr (shd cover most if not all cases).
      This is needed for auto-translating from desktop to webCli. (NOT all useCases but some)
      SEE ALSO the ms js engine; it might suffice.  Try to avoid Fable etc.
      @rsch also js TO Linq; cld be useful to allow clis to add extention mods.""", "", "cr8d:Dec24", "");
    
    Tk24("Brij", "Core", "Server", "Architecture: Cli-Svr Split", """
    (This tk follows consolid8n) Addenda:
      - we nd to split ALL the dataTys to a sep lib so that there are no issues with persistence cli/svr.  Ensure to ONLY incl tys nded cliSide
      - Most stuff in UIAux will go to svr, again graft stuff piecemeal to cli to ensure only stuff we nd.""", "", "cr8d:Dec24", "");
    
    Tk24("Brij", "Other", "General", "Fix mermaid flowcharts 4 Templating", """
    The Templating section has numbered els, as below (1. & 2a.)  Remove the numbers, mermaid now sees them & throws "Unsupported: list" see below:
      ## Templating
      ### Brij flow using ρ setup ->
      ```mermaid
      flowchart LR
          A[1. Table] -->|New<br>DesignTempl| B(2a. DesignCopy<br>Server<br>)""", "", "cr8d:Dec24", "");
    
    Tk24("Brij", "UI", "dzClient", "Font sel (all els) AFTER cssImpl", """
    Add a lookupLi to AdminTbl consisting of all curr googFonts; use 4 select/dropDn.
      Tell dev "We've sel these 2 (primary/sec).  If you wish to apply custom fonts, we have 1735 goog font families to choose from."
      To apply, just subst in the css (Another reason to use v7 strInterpol.)
        @v2: Offer more than 1 (say five) for mult customization; will nd to add them to widNodes via script during bld.  ALSO will nd to add bizLogic 2 dzCli.""", "", "cr8d:Dec24", "");
    
    Tk24("Brij", "UI", "WebClient", "Before testing/impl", """
      Basically the BM is used in 3 scenarios: brij/desktop/Web 
      Some matches will apply _only_ internal e.g. we're using the fontPanel w/btn in brijSettings to allow diz to config the app.  Others will apply to ONLY dizCli; OR statically used by diz for endUser BUT endUser has no way to config/use those wids.  The only feat of poss use to endUsers (nds further thought) is Font, used to color DVrows by formula.""", "", "cr8d:Dec24", "");
    
    Tk24("Brij", "Core", "Server", "HelpSys: HlpDlgContents", """
    Some notes exist on the HlpSys diz, BUT we're better off associating ea img w/opt<title, txt>; stored under unid in AdminTbl.  Bld the imgs in the same way we did for the D3 hlpPanel; store as PSDs so easy to update.
        Note that we can use the imgs in both winForms + webHelpDlg.
        We shd pro'lly wait until clis are ready for testing to impl this (so no changes) BUT we can begin work on the infrastruct webCli: titlePn etc. set up tables/style.""", "", "cr8d:Dec24", "");
    
    Tk24("Brij", "Other", "Marketing", "Freemium tk addenda", """
    "You spent 60 mins to set up your tbls.  Wld you consider spending 10 mins on a survey to save having to do that effort again?" 
    [MUCH more concise txt]""", "", "cr8d:Dec24", "");
    
    Tk24("Brij", "UI", "dzClient", "-dskCli alw fullAuthAcc-", """
             This is HUGE, nds 2 go into archFlowCh somepl.
             Note that the dskCli has _NO Auth for data access_ since all usrs are dzs -- The svr is 'dumb', only sends bare data (localState) OR dats (web) which's validated svrSide
    Earlier notes:
    - The idea is: these users will alw be Designers by default (otherwise they can use another cli)
    - The server is 'dumb'; only sends bare data to richCli (>> stateLocal) OR Dats to webCli
    - Validation for forms/single entry occur cliSide
    - All else (incl qEd) occurs svrSide
    - Consider MongoAuth since we're not doing much with the DB + auth is fldLevel so it'll be cleaner than impl functionality ourselves""", "", "cr8d:Dec24", "");
    
    Tk24("Brij", "UI", "WebClient", "feature-parity", """
    Note that some winCli features DO NOT have equiv webCli equivs.
    Ensure all features are present in both
    See brijLog23 entry 4 Jul 22  Mod test/winFrms.fs""", "", "src:Nov24_Notes", "");
    
    Tk24("Brij", "UI", "MM", "FldDef Ty changes", """
    @Chk: FldDef nds defaultVal + handler based on ty 
    Also, frmDz unAuth access needs BOTH 
    - Blank val + Disabled cell (whole thing)
    """, "", "", "11-29-2024");
    
    Tk24("Brij", "Other", "Sales", "Sales: Freemium", """
    - By default all features available PLUS 3 full working demo dbs to tinker with, copied to user's new acct.
    - Free ver allows no saving data.
    - For the 1st 10 times, IF user chooses Save to CSV >> prompt ""We can save all your data/defs if you complete a short survey""
    - This is a win-win: saves you time to reimport/setup and gives us feedback
    - Flag turned on - userCateg ""FreemiumWithSave"" + track no. of saves/etc.
    - After 10 saves/surveys do a hard sell OR move to CSV""", "", "src:Nov24_Notes", "");
    
    Tk24("Brij", "Other", "Marketing", "Research setup act/Medium", """
    - Chk if it remains The best for free usage
    - Prod info articles etc.""", "", "src:Nov24_Notes", "");
    
    Tk24("Brij", "Other", "Marketing", "Sups @ [hindAltPoplar] Tot", """
    can begin anytime""", "", "src:Nov24_Notes", "");
    
    Tk24("Brij", "Core", "Server", "Agents: Add docLvl Trigger Points", """
    - Any sophisticated workflow nds triggers on Save/Create/CronTime
    - e.g. consider eBay like bids requiring (i) Deadline (ii) Rules
    - @ deadline the Agent runs & applies rules
    - @ Save rules applied (e.g. Reserve Price)""", "", "src:Nov24_Notes", "");
    
    Tk24("Brij", "Other", "Sales", "Demo Dbs", """
    - Give em 3 full examples
    - One cld be tkL with customization e.g. only Mods w/Prog Widgt + some custom (ALL WITH WALKTHRUS)
    - One with 1M+ recs (as designed)
    - One like CraigsList/similar/idea with `Motorbikes for $20 (free meter if you spend 1k+)`
    Earlier Notes:
     Poss utility in separate demos 4 scenarios
     ReddHat
     Find online src 4 canned order frm; use/reImpl
     Canned Db example (v2?): Need one with 'Orders' embedded (n.b. 32M limit so no go for blogs CMSs etc.) WITH example embedded form, full functionality""", "", "src:Nov24_Notes", "");
    
    Tk24("Brij", "Core", "Server", "Architecture: DzDocs", """
    - We nd to convert all DzDocs to docId refs
    - CustId + TableID (say max 100 Tbls)
    - Cld use CustId ctor returns Pty to pass ev.where
    - CustID is SEPARATE from UserID (1 - many; Cust is Tenant)""", "", "src:Nov24_Notes", "");
    
    Tk24("Brij", "Other", "Sales", "Pricing Per Tenant Svr", """
    - So the link which embeds the webCli DVs shd reside on a single svr
    - This allows separation by, say, venture
    - For certain clis we'll need to offer ability to use an IP mask""", "", "src:Nov24_Notes", "");
    
    Tk24("Brij", "Core", "Infra", "dotNet Bundle Reqmts", """
    Nd to chk bundle reqmts 2 run dotNet in new env. 
    (Note that other tasks e.g. Svr depend on this task)""", "", "src:Repo_Gist", "");
    
    Tk24("Brij", "UI", "dzClient", "DnD Impl", """
    Dynamic bld (completed 10/11/23)
     bld operational, tested (completed 10/11/23)
     Monadic state upd8t on DnD""", "", "src:Repo_Gist", "");
    
    Tk24("Brij", "UI", "dzClient", "Wireframes Tabbed PropBox", """
    reimpl frm w/declarative|domConstr
     New Tys 4 tabbedPgs
     New Tys 4 PropBox (bld via above)
     New Ty incorp8ing all propParams >> dzEl
     Make ctxtMenus modular, test w/curr setup
     Reuse/reImpl 4 other els
     Use ResX for assets""", "", "src:Repo_Gist", "");
    
    Tk24("Brij", "UI", "WebClient", "Wireframes WebCli", """
    Impl/test Grid. See <a href=""https://trivedienterprisesinc.github.io/CP_Logic.txt"">Notes</a>
     Impl barebones working test ver w/all func necc.
    [_] Refactor code to accept direct defs
     Refactor code to accept dat in more or less similar fmt
     Ensure all necc mods in place (auto-col-resize etc.)
     Essentially all func shd mirror Desktop + use same params (Cleaner on svrSide)
     Impl/test Forms. See form tester
     Hold off for now on impl Auth""", "", "src:Repo_Gist", "");
    
    Tk24("Brij", "Core", "Server", "Import Module", """
    We nd an export mod too; the main task here is to determine which internal flds to expose (Pros: easy rebuilding of cli backed-up dbs; Cons: exposure) Basic func only (also nded for ReddHat)
     Impl/test; just basic func will do this is the last major mod left to complete
     File|New >> 'Do you have a data dump (CSV) file? 'hlp:what is this?' ie, yes/no dlgBoxes; no wizardLike stuff.
     Ideally use continuationMonad to terminate if errors > preset %age limit of totalInputSz i.e., deterministic flow with prompt + option to continue/break""", "", "src:Repo_Gist", "");
    
    Tk24("Brij", "Other", "Downloads", "Setup Client Downloads", """
    AutoDownload (cli+dsk) on 1st login""", "", "src:Repo_Gist", "");
    
    Tk24("Brij", "Core", "Client", "DbClipboard Impl/test", "", "", "src:Repo_Gist", "");
    
    Tk24("Brij", "Core", "Server", "Earlier Agent Notes", """
     Impl MessageBox queue to proc
     Will run as Indep svc full-time on svr
     Will def nd 2 offer users ability to trigger on Save/Update
     Offer some canned fns e.g. sendMail aka transactional emails
     Offer triggers 4 onEvent|manualRun|hourly|daily|wkly|monthly
     We have sketched out the types for this elsewhere (@ToDo: loc8) what's key is LastRunOn and LastRunStatus; the currRn will collect docs/filter based on these flds.
     The assoc dox 4 dbFuncs will go into the AdDb (nd to impl ids etc.)
    No immed/manual; set min @ 15 min (this is 4 RunOnce) then hourly/daily/wkly/monthly
    
    (If not polling; load'll be much less; this just runs on a freq.)
    
    Trigger proc to runOnSvr ev 15m; look at dbFunc docs (AdTbl)
    1 if (intvl = runOnce) && (LastRunOn != never), del doc
    2 if (timeSinceLastRun > intvl), put in msgBox Queue
    3 After run, if succ, push (LastRunOn; LastRunStatus) to dbFuncDoc
    4 dbFuncs'll nd to run Qrys e.g. for (newCustomers w ord > $1k since lastRun)
    For now just gen list to cons; l8r we can setup service queues.""", "", "src:Repo_Gist", "");
    
    Tk24("Brij", "Core", "Server", "mainAdTbl", """
    We can pro'lly use a 'MainAdTbl' which's a storehouse 4 stuff like cliInfo/Keys (no outside access); & this cd host the dbFuncDocs If there're too many can be isolated per case.""", "", "src:Repo_Gist", "");
    
    Tk24("Brij", "Other", "Research", "Myers/Briggs & current updates", """
     How 2 best ask pointed Qns/gather info?
     Tailored decision trees per user ty?
    Taking Kiersey further (farther?) @rsch motivation: prompts, what kind nded when, application, results.""", "", "src:Repo_Gist", "");
    
    Tk24("Brij", "Other", "Research", "Auth: Mongo Realm", """
     Mongo realm/ roll-yr-own (lk 4 c#) must accomodate webTkns
     look also @repl/GitH setup on their repo""", "", "src:Repo_Gist", "");
    
    Tk24("Wwnn", "Research", "General", "Filter on ELO rating?", """
    ELO rtng: how 2 filtr (utils exist?) (3 lvls; determine which; filter out bottom)
    How 2 use existing bd for this?
    2 btns: 1st shows next, 2nd shows curr
    Nd beaucoup articles/refs/lit: publicDomain, embed. All of CM is evidently pd (confirm)""", "", "src:Repo_Gist", "");
    
    Tk24("Brij", "Core", "Server", "AttMod Initial Notes", """
    Attachments	Img/Attachment hndlrs 4 svr: modify staticHndlr to recv docId: url/img/docId/nm.ext & pull from relevant doc. This shd work whether or not we use a specific imgTbl""", "", "src:Repo_Gist", "");
    
    Tk24("gfx", "General", "var", "Various", """
    For rtEd; find an appropriate widget to embed & see before/after parsing results in all modes. Consider regExRepl 4 customEls, p'haps w/props exposed for advanced usrs to edit in-place
    Flatten types of content to just one; simpler overall
    @rsch bookmark fileFmt 4 all browsers
    No ver capab 4 now, v2+
    Random pnlHdr colors assigned based upon last of differentiators; customizeable by usr
    TopPnl shd incl srchBtn offering ftSrch capab; sorted by count (as b4); ttip in ctxt -> reimpl in Fs""", "", "src:Repo_Gist", "");
    
    Tk24("gfx", "General", "General", "Mermaid/Chart support", """
    b.html update: nd to embed all flowcharts using mermaid b4 Notes.md can be retired.
    Nd to program ability to embed via extension + Button""", "", "src:Repo_Gist", "");
    
    Tk24("Brij", "UI", "MM", "Validation Rules", """
    on Compose: see if we can use the ?ing Form Funct w/fns (for hlp popup)""", "", "src:Repo_Gist", "");
    
    
    Tk24("Brij", "Core", "Server", "Computed Fields (v2?) @tibbie", "", "", "src:Repo_Gist", "");

    Tk24("Brij", "Core", "Server", "LookupDocs @tibbie", "", "", "src:Repo_Gist", "");

    Tk24("Brij", "UI", "dzClient", "DzDV: various itms", """
    - 1st DV is deflt (?@tbd); ability to set in CalcDef: checkBoxes 
    - new TSBtn ""Create Copy"" for dzDox 
    (upd Sept18_23: No, just allow access transparently via normal tbar Cut/Cpy/Paste btns)
    Modify st.Bind to call >!> * ? {let x = get; do! ? {...}
    * Q: can we avoid using x (bind takes f not fx?)"
    """, "", "", "09-18-2023");
    
    Tk24("Brij", "Core", "Server", "Aux var", """
    Do amish 4 3 lists; 1st all 8 & 2nd half-ish if (1) elif (2) elif (3)...
    NO existing replaced 4 now
    After in place mt be a good time to clean mods & move new Core fns in since we now have a working dll set.""", "", "src:Repo_Gist", "");
    
    Tk24("Brij", "Core", "Server", "Brij.Expr", """
    The only place we act'ly nd dyComp is after types change in ???
    The rest can/shd be handled via exprShape
    @ToDo test embedding exprs within others (shd work)
    @ToDo find best way to store these
    @ToDo determine whether a sep expr is nded for crit.""", "", "src:Repo_Gist", "");
    
    Tk24("Brij", "UI", "General", "var UI itms", """
    Note module Form uses Task etc. types from Brij; consider moving entire mod to a separate fork
    
    recr ty / basic frame; add pointLess, fix w/Dirs, no comb
    new tag/tpl/st single runC in ea Mod
    sketch out basic data + popDat
    bur p x y shd rem matches (?)
    tst: push 3 params + Comb 2 tags; put thro + brush
    simulate via map/list/whatever
    if comp compl use cq |> eval
    ONCE above verified -> basic 5 (3param) + add'l 4(or as nded) -> Commando mode""", "", "src:Repo_Gist", "");
    
    Tk24("gfx", "UI", "gridx_Styling", "initFrm: Find hooks to upd rec x to y of zz (not sel; tots) [@Minor]", """
    (Pls. see the gridx repo <a href='https://trivedienterprisesinc.github.io/ui/2024/form/gridx_Styling/gfxGridx.html'>pg</a> for ctxt)<br>
    <ul>
    <li><input id='categRowSelect' type="checkbox" checked>&nbsp;selectedRows / chkboxes for categRows<br>
    <br>
    <mark>Oct29</mark> Quasi-impl y'day, @mbi BUT the UnSel mod does this precisely PLUS autoCorrects the Totals (bonus!)<br>
    As usu., found a cleaner impl: modified treeNestedGen to create a ob of categIds for eventual push 2 grid ctor (all that's reqd.) <mark>@ToDo</mark>Port this svrSide in getDat<br>
    Css: as-is works but ideally p'haps set HdrRowChkBxStyle to (opaque = 0) see gridx.css default is .5 for disabled<br>
    <s>Earlier notes:<br>
    We nd 2 hide chkboxes for categRows (this also impacts the <b>selectedCount/totRecs</b> in the footer.  Don't clobber the existing number, see if we can hook into/change the number by subtracting categs) (UnSel Mod does this, see Oct 29 above)<br>
    The main <mark>issue</mark> gridx treats CategR as a row (with data + children), we don't.  <b>Consider</b>: What's the easiest way to make the existing code disregard categRows?
    
    <ul><li>one approach: See gridx <a href='https://oria.github.io/gridx/demos/unselectableRow.html'>unSelectableRows</a> (shows disabled chkBox)<br>
    [However, this appears perhaps <b>confusing</b> from a usr perspective.  Consider hiding it entirely based on <i>isCateg</i>]</li><li>
    <mark>@v2?</mark> Poss util in allowing users to click a special (diff color?) chkbox to (de)select <b>only</b> items in that group/subgroup (nds to add functionality 2 existing gridx.select mod & work in tandem)<br>
    <i>@TBD: This may be confusing too</i>  A rt-click-ctxt-Menu probably b8r.</s>
    </li></ul>
    </li>
    <li><input type="checkbox" checked> Update enabled state of tbActnBtns via connect.onSelectionChange</li>
    </ul>
    """, "", "AffectsBrijsrc:gridx_Styling", "");
    
    Tk24("gfx", "UI", "gridx_Styling", "qEd + frmValid8n [@EffortWorthy]", """
    (Pls. see the gridx repo <a href='https://trivedienterprisesinc.github.io/ui/2024/form/gridx_Styling/gfxGridx.html'>pg</a> for ctxt)<br>
    <ul>
    	<li>Begin by Impl.ing a placeholder dlg</li>
        <li>Note that the dat already has the cols</li>
    		<li>
    			<h3>Validation</h3>
    
    <details><summary><mark>Oct31</mark>&nbsp;Approach #3</summary>
    <mark>Nov01</mark>&nbsp;There are edge cases where this can't be all cliSide:
    <ul><li>
    If the cli selects recs from diff pgs (e.g., pg 1 & pg 51) the store won't contain all recs (<b>unless</b> we use a 'selectDV' but that's a whole diff can o'worms) so the chk has to be svrSide.  Also if cli selects <b>All</b> [this is also an issue with impl a selectDV: cli might crash if > 1millRecs]
    </li><li>The cleanest approach seems to be making <b>all</b> qEd valid.n svrSide rather'n splitting the flow; <i>even though</i> the cliSide still nds the js validationFns for the Form,
    </li></ul>
    <br>
    Oct 31 Impl. notes -><br>
    <ul><li>
    dojo allows adding custom validator fns.
    </li><li>Impl the whole flow using a mock layoutStruct (to be gen svrSide - <b>Note:</b> frmGenerator now nds to gen js fns; pumped 2 places: struct + frm)
    </li><li>Add validatorFn + invalidMsg flds to the struc
    </li><li>Dynamically assign in qEdDlg (tested Oct30, works fine)
    </li><li>In template for structGenerator: Set init fldSel to blank; return false; msg: Pls choose fld.
    </li></ul>
    </details>
    
    <details><summary><mark>Oct30</mark>&nbsp;Approach #2</summary>
    
    &nbsp;Ignore the earlier notes below.  A Func approach demands clean data from the edges; validation <b>HAS</b> to occur cliSide, svr will fail silently on invalid data.  New approach:
    <ul><li>
    We nd to translate & gen validation fns svrSide and <i>use the same fn for both frm + qEd (+ poss more feats)</i>
    
    </li><li>Payload gets an ob: <b>key=fldNm val=validationFn</b>; used in gen (if decided to gen frm svrSide it's prePopulated)
    
    </li><li>We *DON'T* nd spec handling for input type="checkbox"/"radio"/"password"/txtArea because they can't be validated, only required.
    
    </li><li><mark>@Chk:</mark> select dropdns (incl ones which allow entry) poss nd special handling.
    </li><li><b>ALL</b> other wids are input type="text", see DateTextBox below:
    </li></ul>
    <small><code>
    	&lt;input class="cellWid dojoFormValue" type="text" name="date" id="date" value=""
    		data-dojo-type="dijit/form/DateTextBox"	observer="logD" &gt;
    </code></small><br>
    
    You can attach a <a href='https://dojotoolkit.org/reference-guide/1.10/dijit/form/ValidationTextBox.html#generate-regular-expressions'>regexpGenerator</a> fn (returns str) to a field as below:<br>
    <small><code>
    require(["dojo/parser", "dijit/form/ValidationTextBox"]);
    
    var after5 = function(constraints){
        var date = new Date();
        if(date.getHours() >= 17){
            return "//d{5}";
        }else{
            return "//D+";
        }
    }
    ...<br>
    &lt;label for="zip2"&gt;Also 5-Digit U.S. Zipcode only:&lt;label&gt;
    &lt;input type="text" name="zip" value="00000" id="zip2" required="true"
        data-dojo-type="dijit/form/ValidationTextBox"
        data-dojo-props="regExpGen:after5, invalidMessage:'Zip codes after 5, county name before then.'" &gt;
    </code></small><br>
    In the above code, <mark>after5</mark> nds to be dynamically assigned to the qEd dlg.  So prog approach instd of declarative?
    </details>
    
    <details><summary>Approach #1</summary>
    	Note: <b>validation</b> nds to be accounted for, on the svr side, + errHandling et al<br>
    		Currently we're handling all valid8n on cliSide. How to ensure qEd inputs valid?<br>
    		One possible approach:<br>
    		<ul><li><s>Add <i>'ndsValid8n'</i> fld to dat, Bool, default false, set to true @ 1st valid8n assigned by dev</s><br>		Sep13: No, use the ob below & if (ob==={}} |> skip</li>
    		<li>Further, add an object like this: 
    			<i>{ fld1: "formula1", ...}</i> 
    			to the dat.</li>
    		<li>Do a lookup within qEd logic to assign.</li>
    		<li>If we're doing this cliSide the qEd dlg will use a <b>frm</b></li></ul>
    		Another poss approach:<br>
    		<ul><li>Handle it svrSide (ie let it fail & inform usr)</li> 
    		<li>Nd a robust dlgBox here as decided, with "20 of 50 records failed validation" </li>
    		<li>+ Commit | Rollback/Abort 
    		<li>+ ability to chk which ones failed + <u>why</u> (docLinks? repurpose verDlgBox?)
            </li></ul>
    </details>
    </ul>
    
    	</li>
    </ul>
    """, "", "AffectsBrijsrc:gridx_Styling", "");
    
    Tk24("gfx", "UI", "gridx_Styling", "widCoverage [Requires SaaduP upd8s]", """
    (Pls. see the gridx repo <a href='https://trivedienterprisesinc.github.io/ui/2024/form/gridx_Styling/gfxGridx.html'>pg</a> for ctxt)<br>
    
    <details>
      <summary><input type="checkbox" name="" value=""> <mark>grid.dat updates</mark></summary>
    <ol>
    
    <li>Impl. Push Updates
    	<ul><li>For this, 1st apply cliSide updates to chk everything works as expected.<br>
    <input type="checkbox" checked>, <mark>Sep09</mark> see [update Row#1] btn above the grid.<br>Note: this snippet uses oria's setup (not our store, api changes)</li>
    <li>Next, apply mock svr updates directly to local store<br>
    <input type="checkbox" checked>, <mark>Sep10</mark> see [setStore] btn above the grid.</li>
    <li><input type="checkbox"><mark>@v2?</mark> <b>Related</b>: Flash 4 live upds</li>
    </ul>
    </li>
    
    
    <li><input type="checkbox">&nbsp; AFAIK dat doesn't currently hold defFrm details; <mark>@TBD</mark>: Are we going to patch it into the dat or directly via req/response?<br>
    Sep20: decided; <b>but</b> for the SaaduP we nd to: 
    <p id='widCoverage' style="margin-left: 40px"><input type="checkbox">&nbsp;complete ev poss wid to hook into the bldr.  And since Completion reqs adequate testing, we nd to upd the generator as well<br> (<mark>@TBD</ark>: use tkFldNms for eventual testing with that dataset?)
    	<ol style="margin-left: 40px">
    		<li><input type="checkbox">&nbsp; Incl all tkFld wids</li>
    		<li><input type="checkbox">&nbsp; Make em confirm 2 existing FM params</li>
    		<li><input type="checkbox">&nbsp; Add earlier tbl fmting (2cols)</li>
    		<li><input type="checkbox">&nbsp; Add missing wids from FM example</li>
    		<li><input type="checkbox">&nbsp; Create tbl (here?) to document all variations</li>
    		<li><input type="checkbox">&nbsp; Gen test data, update testDatSet; ensure each works</li>
    		<li><input type="checkbox">&nbsp; Impl extra customObsvrs as needed</li>
    	</ol></p>
    <p id='CannedWids' style="margin-left: 40px"><input type="checkbox">&nbsp;<b>Canned Widgets</b><br>
    Important: beauc. utility in providing max options for col formats like "Rating" (stars et al); Easy to impl now that we have the image cols working...
    </p>
    	
    	<hr>
    </li>
    <li><mark>@Chk</mark> Does the dat currently have <b>defFrmId</b>? We nd a mechanism to populate this with either userChoice or default/only/genOnDemand.
    </li><li>[<input type="checkbox" checked> Yes, in %age] Does the dat currently have <b>colWids</b>? If not, we nd to add, in %age
    </li>
    <li><input type="checkbox">&nbsp; <mark>@TBD</mark> (Sep20: poss unnecc; "beware of premature optimization")  We nd to persist <b>TopId</b>, here's why:
    <ul>
    <li>User is working with a record and does something to trigger svrRefresh</li>
    <li>Meanwhile other users update 5 recs <b>before</b> TopRec</li>
    <li>The screen will seem to 'jump' down 5 (because the db has updated)</li>
    <li>This update also poss nded for v1; just add a { TopRec:unid } 2 the dat</li>
    <li>Also nd to <b>test</b> for ea eventuality (topUnid no longer exists? fallback to renderedVw)</li>
    </ul>
    <li><input type="checkbox" checked>&nbsp; Impl ExpAll Mechanics<br>
    <ul>
        <li>@TbD: isn't this just a svr call?</li>
        <li><b>Yes</b>, it is<br>
    svr.req(params, 'expAll') &gt;&gt; model update &gt;&gt; done</li>
    </ul></li>
    </ol>
    </details>
    <p>
    """, "", "AffectsBrijsrc:gridx_Styling", "");
    
    Tk24("gfx", "UI", "gridx_Styling", "CannedWids [@EffortWorthy]", """
    (Pls. see the gridx repo <a href='https://trivedienterprisesinc.github.io/ui/2024/form/gridx_Styling/gfxGridx.html'>pg</a> for ctxt)<br>
    
    <p id='CannedWids' style="margin-left: 40px"><input type="checkbox">&nbsp;<b>Canned Widgets</b><br>
    Important: beauc. utility in providing max options for col formats like "Rating" (stars et al); Easy to impl now that we have the image cols working...
    </p>
    """, "", "AffectsBrijsrc:gridx_Styling", "");
    
    Tk24("gfx", "UI", "gridx_Styling", "ModifyFrm5 [@Minor]", """
    (Pls. see the gridx repo <a @Chk <a href='https://trivedienterprisesinc.github.io/ui/2024/form/gridx_Styling/gfxGridx.html#whaaat'>this</a> (backported @mbi?)
    """, "", "AffectsBrijsrc:gridx_Styling", "");
    
    Tk24("gfx", "UI", "gridx_Styling", "mockDeltas", """
    (Pls. see the gridx repo <a href='https://trivedienterprisesinc.github.io/ui/2024/form/gridx_Styling/gfxGridx.html'>pg</a> for ctxt)<br>
    
    [These are all demo data anyway; wait for adeq. dataSet (tk?)]
    <p id='mockDeltas'>
    <ul><li>
    <input type="checkbox"> Expand (mapFn?) existing pvDat to add Add mock deltas to pvDataSet.  We probably want <b> reverse</b> order: ie, newest on top</li>
    <li><input type="checkbox" checked> <mark>Sep21</mark> Add mock fetch fn 2 provide pv dataSet for one rec<br>
    <b>Note Sept21</b>: However, not likely to use it (too cumbersome w/fetch chaining...) </li>
    
    <li><input type="checkbox" checked> <mark>Sep16</mark> <b>add</b> %age widths to grid + hideCols via auto</li>
    <li><input type="checkbox" checked> <mark>Sep16</mark> Fix pvDlg layout (cols aren't resizeable <i> shd be so coz they don't change + systemDlg</i>; <b>add</b> %age widths</li>
    
    <li><input type="checkbox" checked> <mark>Sep23</mark> Add mock fetch priorVersions data for testing (generate from existing test recs)<br>
    
    <li id='populatePR'><input type="checkbox"> Add logic to populate frm w/pv<br>
    dblClick on pV >> pvDlg.hide() >> popul8 underlying dlg w/pV (disabled els)<br>
    			<mark>@TBD</mark>: Do we nd to pass dlgHandle 4 this?  Any poss conflict w/multipleDocs?</li>
    </ul>
    <p>
    """, "", "AffectsBrijsrc:gridx_Styling", "");
    
    Tk24("gfx", "UI", "gridx_Styling", "csvExport [@Minor]", """
    (Pls. see the gridx repo <a href='https://trivedienterprisesinc.github.io/ui/2024/form/gridx_Styling/gfxGridx.html'>pg</a> for ctxt)<br>
    See <a href='https://xcellerant.wordpress.com/2015/01/22/gridx-in-xpages-27-exporting-to-excel/'>this</a><p>
    """, "", "AffectsBrijsrc:gridx_Styling", "");
    
    Tk24("gfx", "UI", "gridx_Styling", "persistMod [@Minor]", """
    (Pls. see the gridx repo <a href='https://trivedienterprisesinc.github.io/ui/2024/form/gridx_Styling/gfxGridx.html'>pg</a> for ctxt)<br>
    <details>
      <summary id='persistMod'><input type="checkbox" name="" value="">: Persist mod</summary>
    <ul>
        <li>Maintaining Grid <a href='https://xcellerant.wordpress.com/2015/01/13/gridx-in-xpages-21-maintaining-grid-state-between-sessions/'>State</a> Between Sessions<br> (gridx saves by id which we can push to userid.cfg)</li>
    	<li>  saves the foll:   
    		<ul><li>Sorting (single and nested)</li>
                        <li>Column Widths</li>
                        <li>Filtering</li>
                        <li>Reordered Columns</li>
                        <li>Hidden Columns</li></ul>
    	</li>
    	<li><input type="checkbox" checked> Persist tested with oria's test setup; works as exp; <br>
    <b>BUT</b> won't work with local file (poss coz the cookie url refers 2 oria, hence doesn't find our settings.</li>
    	<li><mark>@TBD</mark> <mark>@v2</mark> Persist expand/collapse state? (hook into the Persist mod)</li>
    	<li><input type="checkbox" name="" value="">: Poss above nded for <b>v1</b>: we only nd 2 persist openCats.</li>
    	<li>Note also our add'l persist reqmts (TopRow etc.) Nd to use staticFS to test/impl the procs.</li>
    </ul>
    </details>
    <p>
    """, "", "AffectsBrijsrc:gridx_Styling", "");
    
    Tk24("gfx", "UI", "gridx_Styling", "btnTtips", """
    (Pls. see the gridx repo <a href='https://trivedienterprisesinc.github.io/ui/2024/form/gridx_Styling/gfxGridx.html'>pg</a> for ctxt)<br>
    <p>
    
    <details>
      <summary><input type="checkbox">&nbsp; btnTtips</summary>
    gridx css rules show abbrv ttip; nd to change or remove to show real one<br>
    @TBD: Poss utility in extending widBase and attaching ttip based on class OR using a universal handler thus: qry(.hasTtip) &gt; forEa (attachTtipRnner(nd))<br>
    <mark>Sep20</mark> gridx uses <a href='https://developer.mozilla.org/en-US/docs/Web/Accessibility/ARIA/Attributes/aria-label'>'aria-label'</a> (see for ex. the filterBar <a href='https://github.com/oria/gridx/blob/master/templates/FilterBar.html'>templ</a>) This's meant to be used w/btns with no visible descr.  Our ttips do provide info, so poss utility in relabelling all template occurances to <b>aria-describedby</b> or aria-description (see mdn doc above), which shd prevent the abbrev tag from working in gridx.<br>
    <mark>Sep23</mark> Actually, pro'lly don't nd to worry abt this till v2; ttip is visually much bolder; both can coexist...
    </details>
    <br>
    >>     let ttipsWobbly = fun (l:list<string*list<string>>) -> ...
    Nd cell-based ttip read/disp (tho we pro'lly nd work completed on 
    svrSide toJson/wobbly before continuing with this tk)  Currently 2 vers of toWebOb: manual & using JsonWriter (latter pref)
    
    <mark>Nov05_24</mark><br>
        <b>brijLinks</b>: new wid define declaratively via class; creates & attaches on.once().  Add tbBtn 2 create; allow creation for NOT ONLY docs but views, settings, etc. (esp.ly useful to embed in emails)
    
    """, "", "AffectsBrijsrc:gridx_Styling", "");
    
    Tk24("gfx", "UI", "gridx_Styling", "priorVer Phase II [@EffortWorthy]", """
    (Pls. see the gridx repo <a href='https://trivedienterprisesinc.github.io/ui/2024/form/gridx_Styling/gfxGridx.html'>pg</a> for ctxt)<br>
    <ul>
        <li id='pvPhase2'><input type="checkbox"> <b>PriorVers Phase II</b><br>
        <ol><li>
            <b>No qry</b> for pvs, in rec w/pointers (string fld <i>docHistory</i> "||VerNum|updBy|updOn|fldsChanged|_unid||"). <br>
            This is <u>known</u> data/fmt, no chances of parsingErrs.  All hardCoded.<br>
            Also see <a href='#editHist'>this</a> prior tk in this doc<br>
            <ul><li>
            Build dat & popul8 grid (from dlg) on frm.load
            </li><li>
            Remove Dlg (one less dlg).  
            </li><li>Clicking on "ViewPrior" popul8s curr form with data from that doc</li></ul>
        </li><li>This <b>can't</b> be impl. in v2 coz rebuilding docHistories will be problematic.  Therefore now.  Also affects related tks like populatePriors + promotion.
        </li><li>
        <mark>@Fix</mark> curr pvDlg to show verNum instd of id, rem id from vizCols
        </li><li>retain pvDlg tb in details-embedded grid 
        </li><li>Add 2 logic: 1st entry in that grid shd be for the currDoc (in order to switch back if necc.)  @TBD: use a number or just 'current'?
    </ul>
    """, "", "AffectsBrijsrc:gridx_Styling", "");
    
    Tk24("gfx", "UI", "gridx_Styling", "PromoteBtn", """
    (Pls. see the gridx repo <a href='https://trivedienterprisesinc.github.io/ui/2024/form/gridx_Styling/gfxGridx.html'>pg</a> for ctxt)<br>
    <b> [Best compl in conj w/SaaduP upd8s 4 testing]</b><br>
    
    <ul>
    <li><input type="checkbox" checked> <mark>Sep21</mark> Add logic to show/hide appropriate PV btns in frm5
    
    <p id='PromoteBtn'>
    <input type="checkbox"> Add logic to show/hide Promote btn (see below)</p>
    Change initBrijFrm() to incorp foll.:
    <details><summary>snippet</summary>
    			<code><pre>
    			//@ToDo: nd to decide on policy for tBar actionBtns: add/remove or show/hide
    			//	 (IIRC there are issues with show/hide & tBar alignmt)
    			//...disable els...
    			if (doc.isPriorVer){
    				disable edit/Save btns
    				add/enable 'Promote' btn ttipTxt: 'Make this doc the curr ver'
    			}else{
    				remove/disable 'Promote' btn
    				doc.hasPriors ? add/enable PriorVers btn : remove/disable same
    			}</pre></code></details>
    </li><li><input type="checkbox"> <b>pvDat</b> <br>
    <input type="checkbox" checked> <mark>Sep21</mark> Modify datGen + pvDatGen to fix ids & confirm to existing data.
    </li><ul>
    """, "", "AffectsBrijsrc:gridx_Styling", "");
    
    Tk24("gfx", "UI", "gridx_Styling", "populatePriors [Follows tk.btnTtips]", """
    (Pls. see the gridx repo <a href='https://trivedienterprisesinc.github.io/ui/2024/form/gridx_Styling/gfxGridx.html'>pg</a> for ctxt)<br>
    <ul>
    <li id='populatePR'><input type="checkbox"> Add logic to populate frm w/pv<br>
    dblClick on pV >> pvDlg.hide() >> popul8 underlying dlg w/pV (disabled els)<br>
    			<mark>@TBD</mark>: Do we nd to pass dlgHandle 4 this?  Any poss conflict w/multipleDocs?</li>
    </ul>
    """, "", "AffectsBrijsrc:gridx_Styling", "");
    
    Tk24("gfx", "UI", "gridx_Styling", "Add 'Copy' button 2 pvDlg [@EffortWorthy]", """
    (Pls. see the gridx repo <a href='https://trivedienterprisesinc.github.io/ui/2024/form/gridx_Styling/gfxGridx.html'>pg</a> for ctxt)<br>
            <i>Since the frm is Disabled, what if the usr nds 2 access a fld val (say longText) <b>within</b> a specific priorVer?</i><br>
            Click on "Copy" &gt;&gt; toaster "Copied 2 clipboard"<br>
            Copies whole obj in format: [fld1DisplayNm:]\n fld1Contents\n ...fld2...<br>
            Add FM func <i>getDisplayFieldNm(internalFldNm:str)</i> &gt;&gt; returns userLblValue from colDef 
    """, "", "AffectsBrijsrc:gridx_Styling", "");
    
    Tk24("gfx", "UI", "gridx_Styling", "var. misc", """
    (Pls. see the gridx repo <a href='https://trivedienterprisesinc.github.io/ui/2024/form/gridx_Styling/gfxGridx.html'>pg</a> for ctxt)<br>
    
    The file linked above lists some other remaining tks (e.g. RTL Impl) not in the `top` list.  
    """, "", "AffectsBrij", "10-31-2024")]

    let toOneLine = 
        fun s ->
            let CRs = Regex.Replace(s, "\n", "~") //, RegexOptions.IgnoreCase)
            let CR2 = Regex.Replace(CRs, "~~", "~") //, RegexOptions.IgnoreCase)
            let Spcs = Regex.Replace(CR2, "     ", "~") //, RegexOptions.IgnoreCase)
            match Spcs.Length > 500 with
            | true -> Spcs.Substring(0,250) + "\n" + Spcs.Substring(250,250) + "..."
            | _ -> 
                match Spcs.Length > 250 with
                | true -> Spcs.Substring(0,250) + "..."
                | _ -> Spcs

    let addendaAsTks =
        fun addL ->
            addL
            |> List.map (fun t -> 
                        let (Tk24(proj, modu, submod, title, contnt, complOn, tgs, cr8d_for_id)) = t
                        let dt = DateTime.TryParse(cr8d_for_id) |> snd
                        let m = CoreMod(CoreM24((getUNID "^Task"), dt, now(), title, contnt, tgs, ""))
                        Task24([m], proj, modu, submod, "", 0, 0, "", false, defDt, TgtVer(""), "", 1, CustID_Trivedi_TaskTbl())
                    )

    let recsToProc = (addendaAsTks addenda) // @ )
    printfn "recsToProc itms: %A :: %A" (recsToProc.Length) (recsToProc.GetType())

    //we also nd to filter out some of the grps (legacy etc.)

    recsToProc
    |> List.filter (fun itm -> 
                    let (Task24(m, project, moduleNm, subMod, obj, imp, urg, parnt, compl, complOn, TgtVer(tgtVer), docLinks, eLvl, tblTy)) = itm
                    let (CoreMod(CoreM24(DocUNID(unid), crDt, modDt, tit, cont, tags, flag))) = m.[0]
                    project = "Brij")
    |> List.groupBy(fun itm -> 
                        let (Task24(m, project, moduleNm, subMod, obj, imp, urg, parnt, compl, complOn, TgtVer(tgtVer), docLinks, eLvl, tblTy)) = itm
                        let (CoreMod(CoreM24(DocUNID(unid), crDt, modDt, tit, cont, tags, flag))) = m.[0]
                        project + "/" + moduleNm + "/" + subMod)
    |> List.map(fun gping ->
                    let (key, li) = gping
                    printfn "[[[%A (%A recs)]]]" key (li.Length)
                    )
#if onlyCats
                    List.map(fun itm -> 
                        let (Tk24(proj, modu, submod, title, contnt, complOn, tgs, cr8d_for_id)) = itm
                        printfn "|||%A" (title + "|" + tgs + "|||") //plus anything else useful
                        printfn "%A" (toOneLine contnt)
                        ) li)
#end //onlyCats
                        
    
recsToProc |> ser |> printfn "%A"
    printfn "eofaa"
