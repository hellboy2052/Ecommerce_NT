import React, { useState, useEffect } from "react";
import { Button, Form, FormGroup, Label, Input, FormText } from "reactstrap";
import { post_images } from "../../actions/images";
import { useSelector, useDispatch } from "react-redux";
import { get_category_list } from "../../actions/category";
import { create_product, get_product_list } from "../../actions/product";
import { Link } from "react-router-dom";
const CreateProduct = (props) => {
  useEffect(() => {
    dispatch(get_category_list());
    dispatch(get_product_list());
    //checkVar();
  }, []);

  
  const [product, setProduct] = useState({});
  const dispatch = useDispatch();

  const { categoryList } = useSelector((state) => state.category);
  const { productList } = useSelector((state) => state.product);

  var list_category = categoryList.data;

  const CreateProduct = async () => {
    //Set image list
    let images = document.getElementById("images").files;
    const formData = new FormData();
    if (images) {
      for (let i = 0; i < images.length; i++) {
        formData.append("images", images[i], images[i].name);
      }
    }

    formData.append("Name", product.name);
    formData.append("Description", product.description);
    formData.append("Price", product.price);
    formData.append("CategoryId", product.categoryId);
    await dispatch(create_product(formData));
  };

  return (
    <div>
      <Form>
        <FormGroup>
          <Label for="exampleEmail">Name</Label>
          <Input
            type="text"
            name="name"
            id="exampleEmail"
            placeholder="Name"
            onChange={(e) => setProduct({ ...product, name: e.target.value })}
          />
        </FormGroup>
        <FormGroup>
          <Label for="examplePassword">Description</Label>
          <Input
            type="text"
            name="description"
            id="examplePassword"
            placeholder="Description"
            onChange={(e) =>
              setProduct({ ...product, description: e.target.value })
            }
          />
        </FormGroup>
        <FormGroup>
          <Label for="price">Price</Label>
          <Input
            type="text"
            name="price"
            id="price"
            placeholder="Description"
            onChange={(e) => setProduct({ ...product, price: e.target.value })}
          />
        </FormGroup>
        <FormGroup>
          <Label for="exampleSelect">Select</Label>
          <Input
            type="select"
            name="select"
            id="exampleSelect"
            defaultValue="1"
            onChange={(e) =>
              setProduct({ ...product, categoryId: e.target.value })
            }
          >
            {list_category &&
              list_category.map((e, i) => {
                return (
                  <option key={i} value={i + 1}>
                    {e.name}
                  </option>
                );
              })}
          </Input>
        </FormGroup>

        <FormGroup>
          <Label for="exampleFile">File</Label>
          <Input
            type="file"
            multiple
            name="file"
            id="images"
            
          />
          <FormText color="muted">
            This is some placeholder block-level help text for the above input.
            It's a bit lighter and easily wraps to a new line.
          </FormText>
        </FormGroup>
      </Form>
      {/* <Link to='/product'> </Link> */}
      <Button
        color="success"
        onClick={() => {
          CreateProduct();
        }}
      >
        Create
      </Button>
    </div>
  );
};

export default CreateProduct;
