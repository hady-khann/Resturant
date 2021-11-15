module.exports={
    context:__dirname+"/src",
    entry:"./Script.js",
    output: {
        path:__dirname+"/Build",
        filename:"Bundle.js"
    },
    module:{
        rules: [
            {
              test: /\.m?js$/,
              exclude: /node_modules/,
              use: {
                loader: "babel-loader",
                options: {
                  presets: ['@babel/preset-env']
                }
              }
            }
          ]
    }
}


