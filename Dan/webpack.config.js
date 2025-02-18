const path = require('path');
const MiniCssExtractPlugin = require('mini-css-extract-plugin');
const MonacoEditorWebpackPlugin = require('monaco-editor-webpack-plugin');

module.exports = {
	mode: 'development',
	entry: {
		shared_layout: "./Assets/Shared/Layout.ts",
		transfer_create: './Assets/Transfer/Create.ts'
	},
	output: {
		path: path.resolve(__dirname, "wwwroot"),
		filename: "[name].js"
	},
	plugins: [
		new MiniCssExtractPlugin({
			filename: "[name].css"
		}),
		new MonacoEditorWebpackPlugin()
	],
	module: {
		rules: [
			{
				test: /\.ts$/,
				use: "ts-loader"
			},
			{
				test: [/\.css$/, /\.scss$/],
				use: [MiniCssExtractPlugin.loader, "css-loader", "sass-loader"]
			}
		]
	}
}
