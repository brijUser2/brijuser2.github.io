// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

console.log("0. in main.js top b4 import...");
import { dotnet } from './_framework/dotnet.js'
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
const text = exports.MyClass.Greeting();
console.log(text);

const imgTest = () => {
//img test
/*
    console.log("in fn main.imgTest() launching reqB('img')...");
    var arrayBufferView = new Uint8Array( exports.MyClass.reqB("img") );
    console.log("1. (no await) initArray.len = " + arrayBufferView.length);
//    var arrayBufferViewAwait = new Uint8Array( await (exports.MyClass.reqB("img")) );
//    console.log("2. (await) arrayBufferViewAwait.len = " + arrayBufferViewAwait.length);

    var blob = new Blob( [ arrayBufferView ], { type: "image/png" } );
    var urlCreator = window.URL || window.webkitURL;
    var imageUrl = urlCreator.createObjectURL( blob );
    var img = document.querySelector( "#gfx" );
    img.src = imageUrl;
    console.log("3. img.src set...");

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

    console.log("7. in fn main.imgTest() launching reqUrl('img')...");
    //reserved word awaitvar arr3 = new Uint8Array( await (exports.MyClass.reqUrl("img")) );
    var arr3 = new Uint8Array( exports.MyClass.reqUrl("img") );
    console.log("8. (await) arr3.len = " + arr.length);
    var blob3 = new Blob( [ arr3 ], { type: "image/png" } );
    var urlCreator3 = window.URL || window.webkitURL;
    var imageUrl3 = urlCreator.createObjectURL( blob3 );
    var img3 = document.querySelector( "#hulk" );
    img3.src = imageUrl;
    console.log("9. hulk img.src set...");
};

console.log("in main.js: launching imgTest()...");
imgTest();

document.getElementById('out').innerHTML = text;
await dotnet.run();
