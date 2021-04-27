import React, { useEffect, useState } from "react";
import {
  Table,
  Button,
  Form,
  FormGroup,
  Label,
  Input,
  FormText,
  Modal,
  ModalHeader,
  ModalBody,
  ModalFooter,
} from "reactstrap";
import { post_images } from "../../actions/images";
import { useSelector, useDispatch } from "react-redux";
import { get_category_list } from "../../actions/category";
import { create_product } from "../../actions/product";
export default function ProductList(props) {

 
  
  useEffect(() => {
    dispatch(get_category_list());
    //checkVar();
  },[]);
  const [modal, setModal] = useState(false);
  const [imageFile, setImageFile] = useState([]);
  const [fileName, setFileName] = useState([]);
  const [product, setProduct] = useState({});
  const dispatch = useDispatch();
  const toggle = () => setModal(!modal);
  const { categoryList } = useSelector((state) => state.category);
  const { productList } = useSelector((state) => state.product);
  const saveFile = (e) => {
    let arr1 = [];
    let arr2 = [];
    for (var i = 0; i < e.target.files.length; i++) {
      arr1.push(e.target.files[i]);
      arr2.push(e.target.files[i].name);
      // await setImageFile(imageFile.push(e.target.files[i]));
      // await setFileName(fileName.push(e.target.files[i].name))
    }
    setImageFile(...imageFile, arr1);
    setFileName(...fileName, arr2);
  };

  const postImage = async (e) => {
    let count=productList.data.length+1;
    console.log(imageFile);
    console.log(fileName);
    let formData = new FormData();
    for (var i = 0; i < fileName.length; i++) {
      formData.append("ImageFile", imageFile[i]);
      formData.append("FileName", fileName[i]);
      formData.append("ProductId", count);
      //axios.post("https://localhost:44309/api/Image",formData)
      dispatch(post_images(formData));
    }
    console.log(product)
  };

  const postProduct = async() => {
    dispatch(create_product(product))

  };
  var list_category = categoryList.data;
  const checkVar=()=>{
    var countPD = productList.data.length;
    console.log(countPD+ "check");
  }
  
  const submit=()=>{

    postProduct();
    postImage();
    toggle()
  }
  return (
    <Table class="table">
      <thead>
        <tr>
          <th scope="col">#</th>
          <th scope="col">Name</th>
          <th scope="col">Price</th>
          <th scope="col">Image</th>
          <th scope="col">Option</th>
          <th scope="col">
            <Button color="success" onClick={toggle}>
              Create
            </Button>
          </th>
          <Modal isOpen={modal} toggle={toggle}>
            <ModalHeader toggle={toggle}>Product</ModalHeader>
            <ModalBody>
              <Form>
                <FormGroup>
                  <Label for="exampleEmail">Name</Label>
                  <Input
                    type="text"
                    name="name"
                    id="exampleEmail"
                    placeholder="Name"
                    onChange={(e)=>setProduct({...product,name:e.target.value})}
                  />
                </FormGroup>
                <FormGroup>
                  <Label for="examplePassword">Description</Label>
                  <Input
                    type="text"
                    name="description"
                    id="examplePassword"
                    placeholder="Description"
                    onChange={(e)=>setProduct({...product,description:e.target.value})}
                  />
                </FormGroup>
                <FormGroup>
                  <Label for="price">Price</Label>
                  <Input
                    type="text"
                    name="price"
                    id="price"
                    placeholder="Description"
                    onChange={(e)=>setProduct({...product,price:e.target.value})}
                  />
                </FormGroup>
                <FormGroup>
                  <Label for="exampleSelect">Select</Label>
                  <Input type="select" name="select" id="exampleSelect" defaultValue="1" onChange={(e)=>setProduct({...product,categoryId:e.target.value})}>
                    {list_category &&
                      list_category.map((e, i) => {
                        return <option key={i} value={i+1}>{e.name}</option>;
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
                    onChange={saveFile}
                  />
                  <FormText color="muted">
                    This is some placeholder block-level help text for the above
                    input. It's a bit lighter and easily wraps to a new line.
                  </FormText>
                </FormGroup>


              </Form>
            </ModalBody>
            <ModalFooter>
              <Button color="success" onClick={submit}>
                Create
              </Button>{" "}
              <Button color="danger" onClick={toggle}>
                Cancel
              </Button>
            </ModalFooter>
          </Modal>
        </tr>
      </thead>
      <tbody>
        {props.list &&
          props.list.map((item) => {
            return (
              <tr>
                <th scope="row">{item.id}</th>
                <td>{item.name}</td>
                <td>{item.price}</td>
                <td>
                  <img
                    style={{ height: "50px" }}
                    src={`https://localhost:44309${item.imageLocation[0]}`}
                    alt="product image"
                  />
                </td>
                <td>
                  <Button color="info">Update</Button>{" "}
                  <Button color="danger">Delete</Button>
                </td>
              </tr>
            );
          })}
      </tbody>
    </Table>
  );
}
