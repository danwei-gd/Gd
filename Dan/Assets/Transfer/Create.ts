import './Create.scss';
import * as monaco from 'monaco-editor';

const editor= monaco.editor.create(document.querySelector('.transfer-create'), {
	value: "aaa",
	language: "json",
	automaticLayout: true
});


