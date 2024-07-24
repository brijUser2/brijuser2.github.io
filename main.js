// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

console.log("0. in main.js top b4 import...");
import { dotnet } from './_framework/dotnet.js';
console.log("0. in main.js top after import...");

    
const { setModuleImports, getAssemblyExports, getConfig } = await dotnet
    .withDiagnosticTracing(false)
    .withApplicationArgumentsFromQuery()
    .create();

setModuleImports('main.js', {
    window: {
        location: {
            href: () => globalThis.window.location.href
        }
    }
});

const config = getConfig();
const exports = await getAssemblyExports(config.mainAssemblyName);
const text = exports.gfxDotNetEntryPt.Greeting();
console.log("main.js:");
console.log(text);
await dotnet.run();

require(["dojo/query", "dojo/dom", "dojo/on", 
/* For the tree tests*/
"dojo/store/Memory", "dijit/tree/ObjectStoreModel", "dijit/Tree",
"dojo/parser"], function(query, dom, on, Memory, ObjectStoreModel, Tree, parser){
    
    const runReqB = () => {
        //Jul18: does not work; assigns icon which lds to link which seems valid
        //      'blob:http://127.0.0.1:59010/60b06ee6-03f1-462c-838d-099cc2429a7f' but no img
        console.log("in fn main.imgTest() launching reqB('img')...");
        var arrayBufferView = new Uint8Array( exports.gfxDotNetEntryPt.reqB("https://trivedienterprisesinc.github.io/img/Getafix.png") );
        console.log("1. (no await) initArray.len = " + arrayBufferView.length);
    //    var arrayBufferViewAwait = new Uint8Array( await (exports.gfxDotNetEntryPt.reqB("img")) );
    //    console.log("2. (await) arrayBufferViewAwait.len = " + arrayBufferViewAwait.length);
    
        console.log("2. reqB...");
        var blob = new Blob( [ arrayBufferView ], { type: "image/png" } );
        console.log("3. reqB...");
        var urlCreator = window.URL || window.webkitURL;
        console.log("4. reqB...");
        var imageUrl = urlCreator.createObjectURL( blob );
        console.log("5. reqB...");
        var img = document.querySelector( "#ReqB" );
        console.log("6. reqB...");
        img.src = imageUrl;
        console.log("7. img.src set...");
    }
    
    const downloadBytes = () => {
        //Stable as of Jul 24th; correct file downloaded...
        var arrayBufferView = new Uint8Array( exports.gfxDotNetEntryPt.DispatchB("reqBase", "https://github.com/brijUser2/main/raw/main/backup/wasm_delta_Jun27.zip") );//hardCoded countImg; update to Task
        const blob = new Blob( [ arrayBufferView ], { type: "image/png" } );
        const href = URL.createObjectURL(blob);
        const anchorElement = document.createElement('a');
        anchorElement.href = href;
        anchorElement.download = "tmp.png";
        document.body.appendChild(anchorElement);
        anchorElement.click();
        document.body.removeChild(anchorElement);
        window.URL.revokeObjectURL(href);
}


    const runListR = () => {
        console.log("1. in fn main.runListR() launching ...");
        exports.gfxDotNetEntryPt.listResources();
        console.log("2. in fn main.runListR() after ...");
    }

    const runListRitms = () => {
        console.log("1. in fn main.runListRitms() launching ...");
        exports.gfxDotNetEntryPt.listResourceItms();
        console.log("2. in fn main.runListRitms() after ...");
    }
    
    const runImgFromRes = () => {
        console.log("1. in fn main.runImgFromRes() launching ...");
        var resImg = document.querySelector( "#ResImg" );
        var arr = new Uint8Array(exports.gfxDotNetEntryPt.DispatchB("getResourceItm", "gfxLogo"));
        var blob = new Blob( [ arr ], { type: "image/png" } );
        var img =  URL.createObjectURL( blob );
        resImg.src = img;
        console.log("2. array len was: " + arr.length);
        console.log("2. in fn main.runImgFromRes() after ...");
    }    
    
    const runReqBase = () => {
        //Jul18: works; shows correct img
        console.log("2-1. in fn main.runReqBase() launching reqBase('img')...");
        var arr21 = new Uint8Array( exports.gfxDotNetEntryPt.reqBase("https://trivedienterprisesinc.github.io/img/Getafix.png") );
        console.log("2-2. in fn main.runReqBase() (await) arr2.len = " + arr21.length);
        console.log("2-3. in fn main.runReqBase() creating blob");
        var blob21 = new Blob( [ arr21 ], { type: "image/png" } );
        var imageUrl21 = URL.createObjectURL( blob21 );
        var img21 = document.querySelector( "#ReqBase" );
        img21.src = imageUrl21;
        console.log("2-4. in fn main.runReqBase() resource img.src set...");
    }
    /*
        console.log("4. in fn main.imgTest() launching reqBase('img')...");
        //var arr2 = new Uint8Array( await (exports.gfxDotNetEntryPt.reqBase("img")) );
        var arr2 = new Uint8Array( exports.gfxDotNetEntryPt.reqBase("img") );
        console.log("5. (await) arr2.len = " + arr2.length);
        var blob2 = new Blob( [ arr2 ], { type: "image/png" } );
        var urlCreator2 = window.URL || window.webkitURL;
        var imageUrl2 = urlCreator.createObjectURL( blob2 );
        var img2 = document.querySelector( "#getafix" );
        img2.src = imageUrl;
        console.log("6. getafix img.src set...");
    */
    
    const runReqUrl = () => {
       //Jul18: works; prints 48 tree itms to console
        console.log("8. in fn main.imgTest() launching reqUrl('img')...");
        
        //@mpt: this goes to gfx.fs, lists resources & quits @ gfx.fs.eom
        //ie, probably the old ass.
        
        //reserved word awaitvar arr3 = new Uint8Array( await (exports.gfxDotNetEntryPt.reqUrl("img")) );
        var arr3 = new Uint8Array( exports.gfxDotNetEntryPt.reqUrl("https://trivedienterprisesinc.github.io/img/Getafix.png") );
        console.log("9. (await) arr3.len = " + arr.length);
        var blob3 = new Blob( [ arr3 ], { type: "image/png" } );
        var urlCreator3 = window.URL || window.webkitURL;
        var imageUrl3 = urlCreator.createObjectURL( blob3 );
        var img3 = document.querySelector( "#ReqUrl" );
        img3.src = imageUrl3;
        console.log("10. hulk img.src set...");
    }
    
    const runReqUrl2 = () => {
        //Jul18: doesn't work; [[probably due to ref to .resx instd of .resource]]]
            //in reqUrl2; b4 getBytesUsingAsync ...
            //Error: The type initializer for '<StartupCode$Trivedi-Gfx>.$Gfx' threw an exception.
            //at Jn (marshal-to-js.ts:349:18)
        console.log("8. in fn main.imgTest() launching reqUrl2('img')...");
        const imgURL = "https://trivedienterprisesinc.github.io/img/Getafix.png";
        //reserved word awaitvar arr3 = new Uint8Array( await (exports.gfxDotNetEntryPt.reqUrl("img")) );
        var arr3 = new Uint8Array( exports.gfxDotNetEntryPt.reqUrl2(imgURL) );
        console.log("9. (await) arr3.len = " + arr.length);
        var blob3 = new Blob( [ arr3 ], { type: "image/png" } );
        var urlCreator3 = window.URL || window.webkitURL;
        var imageUrl3 = urlCreator3.createObjectURL( blob3 );
        var img3 = document.querySelector( "#ReqUrl2" );
        img3.src = imageUrl3;
        console.log("10. hulk img.src set...");
    }

    const callFsDispatchB = (c, p, d) => {
        console.log("1. in fn main.callFsDispatchB() launching ...");
        var resImg = document.querySelector( d );
        var arr = new Uint8Array(exports.gfxDotNetEntryPt.DispatchB(c,p));
        var blob = new Blob( [ arr ], { type: "image/png" } );
        var img =  URL.createObjectURL( blob );
        resImg.src = img;
        console.log("2. array len was: " + arr.length);
    }

    const callFsDispatchS = (c, p, d) => {
        console.log("1. in fn main.callFsDispatchS() launching ...");
        const tr = exports.gfxDotNetEntryPt.DispatchS(c);
        console.log("tree len: " + tr.Length + "\n" + tr);
        var mod = JSON.parse(tr);
        var myStore = new Memory({
            data: mod,
            getChildren: function(object){
                return this.query({parent: object.id});
            }
        });
        var rt = (c == "loadTree1") ? {id: 'root'} : {id: 'world'};
        var myModel = new ObjectStoreModel({
            store: myStore,
            query: rt
        });
        var tree = new Tree({
            model: myModel
        });
        tree.placeAt(dom.byId("treeDiv"));
        tree.startup();
    }
    
    const runImgAllJS = () => {
        //this works but who nds it? @ToDo: remove l8r
        console.log("in runImgAllJS");
        const imgURL = "https://trivedienterprisesinc.github.io/img/Getafix.png";
        fetch(imgURL)
        .then(response => response.blob())
        .then(blob => {
            document.getElementById('AllJS').src = URL.createObjectURL(blob)
        })
    }
    
    document.getElementById('out').innerHTML = text;

    console.log("Now assigning btn handlers...");

/*
    console.log("Now assigning btn handler1...");
    on(dom.byId("runReqB"), "click", function(e){
        console.log("Clicked on runReqB btn ");
        runReqB();
    });*/
    
    console.log("Now assigning btn handler2...");
    on(dom.byId("runReqBase"), "click", function(e){
        console.log("Clicked on runReqBase btn ");
        runReqBase();
    });
    /*
    on(dom.byId("runReqUrl"), "click", function(e){
        console.log("Clicked on runReqUrl btn ");
        runReqUrl();
    });
    on(dom.byId("runReqUrl2"), "click", function(e){
        console.log("Clicked on runReqUrl2 btn ");
        runReqUrl2();
    });
    */
    on(dom.byId("runListRes"), "click", function(e){
        console.log("Clicked on runListRes btn ");
        runListR();
    });

    on(dom.byId("runListResItms"), "click", function(e){
        console.log("Clicked on runListResItms btn ");
        runListRitms();
    });
    on(dom.byId("loadResItm"), "click", function(e){
        console.log("Clicked on loadResItm btn ");
        runImgFromRes();
    });

    on(dom.byId("runReqUrlTask"), "click", function(e){
        console.log("Clicked on runReqUrlTask btn ");
        callFsDispatchB("reqUrlTask", "", "#ReqUrlTaskImg");
    });
    on(dom.byId("runReqUrlTask2"), "click", function(e){
        console.log("Clicked on runReqUrlTask2 btn ");
        callFsDispatchB("reqUrlTask2", "", "#ReqUrlTask2Img");
    });
    on(dom.byId("loadTree1"), "click", function(e){
        console.log("Clicked on loadTree1 btn ");
        callFsDispatchS("gfxTreeModel", "", "");
    });
    on(dom.byId("loadTree2"), "click", function(e){
        console.log("Clicked on loadTree2 btn ");
        callFsDispatchS("gfxTreeModel2", "", "");
    });
    on(dom.byId("downloadBytes"), "click", function(e){
        console.log("Clicked on downloadBytes btn ");
        downloadBytes();
    });
});