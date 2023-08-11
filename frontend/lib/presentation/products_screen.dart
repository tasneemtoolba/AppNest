import 'package:appnest/api.dart';
import 'package:appnest/domain/product_model.dart';
import 'package:flutter/material.dart';
import 'package:uuid/uuid.dart';

class ProductsScreen extends StatefulWidget {
  const ProductsScreen({Key? key}) : super(key: key);

  @override
  State<ProductsScreen> createState() => _ProductsScreenState();
}

class _ProductsScreenState extends State<ProductsScreen> {
  final _formKey = GlobalKey<FormState>();
  TextEditingController productName = TextEditingController();
  TextEditingController price = TextEditingController();
  @override
  Widget build(BuildContext context) {
    return Scaffold(
      appBar: AppBar(
        title: const Text('Products'),
      ),
      body: SafeArea(
        child: Padding(
          padding: const EdgeInsets.all(15),
          child: Column(mainAxisSize: MainAxisSize.min, children: [
            ElevatedButton(
              onPressed: () {
                showDialog(
                    context: context,
                    builder: (BuildContext context) {
                      return AlertDialog(
                        content: Stack(
                          clipBehavior: Clip.none,
                          children: <Widget>[
                            Positioned(
                              right: -40.0,
                              top: -40.0,
                              child: InkResponse(
                                onTap: () {
                                  Navigator.of(context).pop();
                                },
                                child: const CircleAvatar(
                                  backgroundColor: Colors.red,
                                  child: Icon(Icons.close),
                                ),
                              ),
                            ),
                            Form(
                              key: _formKey,
                              child: Column(
                                mainAxisSize: MainAxisSize.min,
                                children: <Widget>[
                                  Padding(
                                    padding: const EdgeInsets.all(8.0),
                                    child: TextFormField(
                                      controller: productName,
                                      validator: (value) => value!.length > 30
                                          ? 'Please enter a shorter name'
                                          : null,
                                    ),
                                  ),
                                  Padding(
                                    padding: const EdgeInsets.all(8.0),
                                    child: TextFormField(
                                      controller: price,
                                      keyboardType: TextInputType.number,
                                      validator: (value) =>
                                          int.tryParse(value!) == null
                                              ? 'please enter a number'
                                              : null,
                                    ),
                                  ),
                                  Padding(
                                    padding: const EdgeInsets.all(8.0),
                                    child: ElevatedButton(
                                      child: const Text("Add Product"),
                                      onPressed: () async {
                                        if (_formKey.currentState!.validate()) {
                                          ProductModel product = ProductModel(
                                              productName: productName.text,
                                              regularPrice:
                                                  double.parse(price.text),
                                              productId: const Uuid().v1());
                                          Navigator.of(context).pop();
                                          await API().getProducts();
                                          await API().addProduct(product);
                                          setState(() {});
                                        }
                                      },
                                    ),
                                  )
                                ],
                              ),
                            ),
                          ],
                        ),
                      );
                    });
              },
              child: const Text('Add Product'),
            ),
            Expanded(
              child: FutureBuilder(
                future: API().getProducts(),
                builder: (BuildContext context, AsyncSnapshot snapshot) {
                  if (snapshot.hasData) {
                    List<ProductModel> products = snapshot.data;
                    print("number of products = ${products.length}");
                    return ListView.builder(
                      itemBuilder: (context, index) {
                        print("products in future : ${snapshot.data}");
                        return ListTile(
                          onTap: () {},
                          title: Text(products[index].productName),
                          subtitle:
                              const Text('click here to add/remove deals'),
                        );
                      },
                      // verify if cardDetails is null to prevent app crash
                      itemCount: products.length,
                      scrollDirection: Axis.vertical,
                      // controller: _controller,
                      shrinkWrap: true,
                    );
                  } else if (snapshot.hasError) {
                    print("Error here in snapshot");
                    return const Center(child: Text("An error has occurred"));
                  } else {
                    return const Center(child: CircularProgressIndicator());
                  }
                },
              ),
            )
          ]),
        ),
      ),
    );
  }
}
