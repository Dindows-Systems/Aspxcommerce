/*
Copyright (c) 2003-2011, CKSource - Frederico Knabben. All rights reserved.
For licensing, see LICENSE.html or http://ckeditor.com/license
*/

CKEDITOR.editorConfig = function(config) {
    config.filebrowserUploadUrl = window.CKEDITOR_BASEPATH + 'UploadHandler.ashx';
    // Define changes to default configuration here. For example:
    // config.language = 'fr';
    // config.uiColor = '#AADC6E';

    // CKEDITOR.editorConfig = function (config) {
    //    config.extraPlugins = "newplugin";
    //    config.toolbar =
    //    [
    //        ['Bold'], ['Italic'], ['newplugin']

    //    ];
    //};key--contentsCssVal --http://localhost:2008/SageFrame/Editors/ckeditor/contents.css


    config.filebrowserBrowseUrl = window.CKEDITOR_BASEPATH + 'FileBrowser.aspx?path=Userfiles/File&editor=FCK';
    config.filebrowserImageBrowseUrl = window.CKEDITOR_BASEPATH + 'FileBrowser.aspx?type=Image&path=Userfiles/Image&editor=FCK';
    //  config.filebrowserWindowWidth: 800;
    // config.filebrowserWindowHeight: 500; key--contentsCssVal --http://localhost:2008/SageFrame/Editors/ckeditor/contents.css
    config.enterMode = CKEDITOR.ENTER_BR;
    config.toolbar =
    [
        ['Source'], ['NewPage', 'Preview'],
        ['Cut', 'Copy', 'Paste', 'PasteText', 'PasteFromWord'],
        '/',
        ['Styles', 'Format'],
        ['Bold', 'Italic', 'Strike'],
        ['NumberedList', 'BulletedList', '-', 'Outdent', 'Indent', 'Blockquote', 'Image']

    ];

};

//CKEDITOR.replace('editor_id', {
//    filebrowserBrowseUrl: '/browser/browse/type/all',
//    filebrowserUploadUrl: '/browser/upload/type/all',
//    filebrowserImageBrowseUrl: '/browser/browse/type/image',
//    filebrowserImageUploadUrl: '/browser/upload/type/image',
//   
//});