import 'dart:convert';

import 'package:appnest/domain/deal_model.dart';

ProductModel productModelFromJson(String str) =>
    ProductModel.fromJson(json.decode(str));

String productModelToJson(ProductModel data) => json.encode(data.toJson());

class ProductModel {
  String productId;
  String productName;
  double regularPrice;
  List<DealModel>? deals;
  ProductModel({
    required this.productId,
    required this.productName,
    required this.regularPrice,
    this.deals,
  });

  factory ProductModel.fromJson(Map<String, dynamic> json) => ProductModel(
        productId: json["id"],
        productName: json["name"],
        regularPrice: json["price"],
        deals: json["deals"] == null
            ? []
            : List<DealModel>.from(
                json["deals"]!.map((x) => DealModel.fromJson(x))),
      );

  Map<String, dynamic> toJson() => {
        "id": productId,
        "name": productName,
        "price": regularPrice,
        "deals": deals == null
            ? []
            : List<dynamic>.from(deals!.map((x) => x.toJson())),
      };
}
