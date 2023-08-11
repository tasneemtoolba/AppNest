import 'package:appnest/domain/deal_model.dart';
import 'package:appnest/domain/product_model.dart';
import 'package:appnest/domain/score_model.dart';
import 'package:dio/dio.dart';

class API {
  var baseUrl = 'http://localhost:5104';
  var dio = Dio();
  API() {
    dio = Dio();
    dio.options.headers['content-Type'] = 'application/json';
  }
  Future<List<ProductModel>> getProducts() async {
    final url = '$baseUrl/Products'; // Replace with your API endpoint

    final response = await dio.get(url);
    print("response.data");
    print(response.data);
    if (response.statusCode == 200) {
      final List<dynamic> productsJson = response.data;
      return productsJson.map((json) => ProductModel.fromJson(json)).toList();
    } else {
      throw Exception('Failed to load products');
    }
  }

  Future<List<DealModel>> getDeals() async {
    final url = '$baseUrl/Deals'; // Replace with your API endpoint

    final response = await dio.get(url);
    print("response.data");
    print(response.data);
    if (response.statusCode == 200) {
      final List<dynamic> productsJson = response.data;
      return productsJson.map((json) => DealModel.fromJson(json)).toList();
    } else {
      throw Exception('Failed to load products');
    }
  }

  // http://localhost:5104/Scores/string?limit=2

  Future<List<ScoreModel>> getScores(String query, int? limit) async {
    var url = '$baseUrl/Scores/$query?'; // Replace with your API endpoint
    if (limit != null) {
      url += "&limit=$limit";
    }
    print('searchQuery = ');
    print(query);
    final response = await dio.get(url);
    print("response.data");
    print(response.statusCode);
    print(response.data);
    if (response.statusCode == 200) {
      final List<dynamic> productsJson = response.data;
      return productsJson.map((json) => ScoreModel.fromJson(json)).toList();
    } else {
      throw Exception('Failed to load scores');
    }
  }

////http://localhost:5104/Deals/by-product/string
  Future<List<DealModel>> getDealsByProductId(String productId) async {
    var url =
        '$baseUrl/Deals/by-product/$productId'; // Replace with your API endpoint

    final response = await dio.get(url);
    print("response.data");
    print(response.statusCode);
    print(response.data);
    if (response.statusCode == 200) {
      final List<dynamic> dealsJson = response.data;
      return dealsJson.map((json) => DealModel.fromJson(json)).toList();
    } else {
      throw Exception('Failed to load scores');
    }
  }

  Future<void> addProduct(ProductModel product) async {
    final url = '$baseUrl/Products'; // Replace with your API endpoint

    final response = await dio.post(
      url,
      data: product.toJson(), // Assuming toJson converts Product to a Map
    );
    print("add product ");
    print(response.data);
    print(response.statusCode);
    if (response.statusCode == 201 || response.statusCode == 200) {
      print('Product added successfully');
    } else {
      print('Failed to add product');
    }
  }

  Future<void> addDeal(DealModel deal) async {
    final url = '$baseUrl/Deals'; // Replace with your API endpoint

    final response = await dio.post(
      url,
      data: deal.toJson(), // Assuming toJson converts Product to a Map
    );
    print("add deal ");
    print(response.data);
    print(response.statusCode);
    if (response.statusCode == 201 || response.statusCode == 200) {
      print('deal added successfully');
    } else {
      print('Failed to send product');
    }
  }
}
