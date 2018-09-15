'use strict';

const webpack = require('webpack');
const path = require('path');

const bundleFolder = "./wwwroot/assets/";
const srcFolder = "./App/"

module.exports = {
    entry: [
        srcFolder + "index.jsx" 
    ],
    devtool: "source-map",
    output: {
        filename: "bundle.js",
        publicPath: 'assets/',
        path: path.resolve(__dirname, bundleFolder)
    },
    module: {
        rules: [
            {
              test: /\.jsx?$/,
              loader:'babel-loader',
              exclude: /node_modules/,
              query: {
                cacheDirectory: true,
                presets: ["@babel/preset-env", "@babel/preset-react"]
              }
            },
            {
              test: /\.css$/,
              use: ['style-loader', 'css-loader']
            }
          ]
    },
    plugins: [
    ],
    mode: 'development'
};