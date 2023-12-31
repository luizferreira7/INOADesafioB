from flask import Flask, request, jsonify, abort
from model.entities import Stock

import yfinance as yf

app = Flask(__name__)


@app.route('/finance/stock_price', methods=['GET'], )
def get_stock():
    args = request.args

    stock = args['stock']

    stockData = yf.Ticker(stock + ".SA")

    try:
        price = stockData.info.get('regularMarketPrice', stockData.info.get('currentPrice'))
    except:
        abort(404)

    stockPrice = Stock(stockData.info.get('symbol'), price)

    return jsonify(stockPrice.__dict__)


port_number = 5132

if __name__ == '__main__':
    app.run(port=port_number, host="localhost")
