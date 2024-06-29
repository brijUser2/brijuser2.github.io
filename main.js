// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

require(["dojo/query", "dojo/parser"], function(query, parser){

    import { dotnet } from './_framework/dotnet.js'
    
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
    const text = exports.MyClass.Greeting();
    console.log("main.js:");
    console.log(text);
    
    const runReqB = () => {
        console.log("in fn main.imgTest() launching reqB('img')...");
        var arrayBufferView = new Uint8Array( exports.MyClass.reqB("https://trivedienterprisesinc.github.io/img/Getafix.png") );
        console.log("1. (no await) initArray.len = " + arrayBufferView.length);
    //    var arrayBufferViewAwait = new Uint8Array( await (exports.MyClass.reqB("img")) );
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
    
    const runReqBase = () => {
        console.log("2-1. in fn main.imgTest() launching reqB('img') - resources...");
        var arr21 = new Uint8Array( exports.MyClass.reqBase("https://trivedienterprisesinc.github.io/img/Getafix.png") );
        console.log("2-2. (await) arr2.len = " + arr21.length);
        var blob21 = new Blob( [ arr21 ], { type: "image/png" } );
        var imageUrl21 = URL.createObjectURL( blob21 );
        var img21 = document.querySelector( "#ReqBase" );
        img21.src = imageUrl21;
        console.log("2-3. resource img.src set...");
    }
    /*
        console.log("4. in fn main.imgTest() launching reqBase('img')...");
        //var arr2 = new Uint8Array( await (exports.MyClass.reqBase("img")) );
        var arr2 = new Uint8Array( exports.MyClass.reqBase("img") );
        console.log("5. (await) arr2.len = " + arr2.length);
        var blob2 = new Blob( [ arr2 ], { type: "image/png" } );
        var urlCreator2 = window.URL || window.webkitURL;
        var imageUrl2 = urlCreator.createObjectURL( blob2 );
        var img2 = document.querySelector( "#getafix" );
        img2.src = imageUrl;
        console.log("6. getafix img.src set...");
    */
    
    const runReqUrl = () => {
        console.log("8. in fn main.imgTest() launching reqUrl('img')...");
        //reserved word awaitvar arr3 = new Uint8Array( await (exports.MyClass.reqUrl("img")) );
        var arr3 = new Uint8Array( exports.MyClass.reqUrl("https://trivedienterprisesinc.github.io/img/Getafix.png") );
        console.log("9. (await) arr3.len = " + arr.length);
        var blob3 = new Blob( [ arr3 ], { type: "image/png" } );
        var urlCreator3 = window.URL || window.webkitURL;
        var imageUrl3 = urlCreator.createObjectURL( blob3 );
        var img3 = document.querySelector( "#ReqUrl" );
        img3.src = imageUrl3;
        console.log("10. hulk img.src set...");
    }
    
    const runReqUrl2 = () => {
        console.log("8. in fn main.imgTest() launching reqUrl2('img')...");
        const imgURL = "https://trivedienterprisesinc.github.io/img/Getafix.png";
        //reserved word awaitvar arr3 = new Uint8Array( await (exports.MyClass.reqUrl("img")) );
        var arr3 = new Uint8Array( exports.MyClass.reqUrl2(imgURL) );
        console.log("9. (await) arr3.len = " + arr.length);
        var blob3 = new Blob( [ arr3 ], { type: "image/png" } );
        var urlCreator3 = window.URL || window.webkitURL;
        var imageUrl3 = urlCreator3.createObjectURL( blob3 );
        var img3 = document.querySelector( "#ReqUrl2" );
        img3.src = imageUrl3;
        console.log("10. hulk img.src set...");
    }
    
    const runImgAllJS = () => {
        console.log("in runImgAllJS");
        const imgURL = "https://trivedienterprisesinc.github.io/img/Getafix.png";
        fetch(imgURL)
        .then(response => response.blob())
        .then(blob => {
            document.getElementById('AllJS').src = URL.createObjectURL(blob)
        })
    }
    
    const runTests = () => {
        runImgAllJS();
        //listResources(); doesn't find the proc
        //runReqB(); //prob: fs tries to load local_resx_file instd of embedded one
        //runReqBase();
        //runReqUrl(); hangs
        //runReqUrl2();
    }
    console.log("in main.js: launching runTests()...");
    runTests();
    //console.log("in main.js: launching listResources()...");
    //listResources();
    document.getElementById('out').innerHTML = text;
    await dotnet.run();
    
    query("runReqB_Btn").on("click", function(){
        runReqB();
    });
    query("runReqBase_Btn").on("click", function(){
        runReqBase();
    });
    query("runReqUrl_Btn").on("click", function(){
        runReqUrl();
    });
    query("runReqUrl2_Btn").on("click", function(){
        runReqUrl2();
    });
});
