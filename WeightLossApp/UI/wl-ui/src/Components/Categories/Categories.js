import { useState, useEffect } from "react";
import React from "react";
import { constants } from "../../Constants";
import { Modal, Button, Form, Row, Col, Table, InputGroup, FormControl } from "react-bootstrap";
import ChangeCategory from "../Modals/ChangeCategory";

function Categories() {
	// Main state of component 
	const [categories, setCategories] = useState([]);
	const [category, setCategory] = useState();

	const [addShow, setAddShow] = useState(false);
	const addHandleClose = () => setAddShow(false);
	const addHandleShow = () => setAddShow(true);

	const [editShow, setEditShow] = useState(false);
	const editHandleClose = () => setEditShow(false);
	const editHandleShow = () => setEditShow(true);

	const [searchLine, setSearchLine] = useState("");
	const [filterParameters, setFilterParameters] = useState({
		Name: false,
		Id: false,
		Danger: false,
		Type: false
	});

	// Get categories data
	function getCategories() {
		fetch(constants.API_URL + "Category")
			.then(res => res.json())
			.then(data => setCategories(data));
	}

	// onLoad function
	useEffect(() => {
		getCategories();
	}, []);

	// Show add modal function
	function addModalShow() {
		addHandleShow();
		setCategory({
			Name: "",
			Type: "",
			Danger: 0,
		});
	}

	// Show edit modal function
	function editModalShow(category) {
		editHandleShow();
		setCategory(category);
	}

	// Delete item function
	function deleteClick(id) {
		if (window.confirm("Are you sure?")) {
			fetch(constants.API_URL + `Category/${id}`, {
				method: "DELETE",
				headers: {
					Accept: "application/json",
					"Content-Type": "application/json"
				}
			})
				.then(res => res.json())
				.then(
					result => {
						getCategories();
					},
					error => {
						alert("Failed");
					}
				);
		}
	}

	// Sort functions
	function sortByName() {
		if (filterParameters.Name) {
			categories.sort((a, b) => a.Name.localeCompare(b.Name));
			setFilterParameters({
				Name: false,
				Type: filterParameters.Type,
				Id: filterParameters.Id,
				Danger: filterParameters.Danger
			});
		} else {
			categories.sort((b, a) => a.Name.localeCompare(b.Name));
			setFilterParameters({
				Name: true,
				Type: filterParameters.Type,
				Id: filterParameters.Id,
				Danger: filterParameters.Danger
			});
		}
	}

	function sortByType() {
		if (filterParameters.Type) {
			categories.sort((a, b) => a.Type.localeCompare(b.Type));
			setFilterParameters({
				Name: filterParameters.Name,
				Type: false,
				Id: filterParameters.Id,
				Danger: filterParameters.Danger
			});
		} else {
			categories.sort((b, a) => a.Type.localeCompare(b.Type));
			setFilterParameters({
				Name: filterParameters.Name,
				Type: true,
				Id: filterParameters.Id,
				Danger: filterParameters.Danger
			});
		}
	}

	function sortById() {
		if (filterParameters.Id) {
			categories.sort((a, b) => a.Id > b.Id ? 1 : -1);
			setFilterParameters({
				Name: filterParameters.Name,
				Type: filterParameters.Type,
				Id: false,
				Danger: filterParameters.Danger
			});
		} else {
			categories.sort((a, b) => a.Id < b.Id ? 1 :-1);
			setFilterParameters({
				Name: filterParameters.Name,
				Type: filterParameters.Type,
				Id: true,
				Danger: filterParameters.Danger
			});
		}
	}

    function sortByDanger() {
		if (filterParameters.Danger) {
			categories.sort((a, b) => a.Danger > b.Danger ? 1 : -1);
			setFilterParameters({
				Name: filterParameters.Name,
				Type: filterParameters.Type,
				Id: filterParameters.Id,
				Danger: false
			});
		} else {
			categories.sort((a, b) => a.Danger < b.Danger ? 1 :-1);
			setFilterParameters({
				Name: filterParameters.Name,
				Type: filterParameters.Type,
				Id: filterParameters.Id,
				Danger: true
			});
		}
	}

	return (
        <div className="container">
            <div style={{ width: 80 + "vw" }}>
                <h3 className="m-5">This is Ingridients page</h3>
                <div className="container mt-5">
                    <InputGroup className="mb-3">
                        <FormControl
                            aria-label="Default"
                            placeholder="Search"
                            value={searchLine}
                            onChange={e => setSearchLine(e.target.value)}
                            aria-describedby="inputGroup-sizing-default"
                        />
                        <Button className="btn-dark" onClick={ () =>  { 
                            setCategories(categories.filter(i => i.Name.includes(searchLine)))
                        }}>Search</Button>
                        <Button className="btn-dark" onClick={() => getCategories()}>Cancel</Button>
                    </InputGroup>
                </div>
                {/* Create new item modal */}
                <Modal size="lg" centered show={addShow} onHide={addHandleClose}>
                    <ChangeCategory
                        state={addHandleClose}
                        category={category}
                        setCategory={setCategory}
                        getCategories={getCategories}
                        method="POST"
                        title="Add new category"
                    />
                </Modal>
                {/* Edit item modal */}
                <Modal size="lg" centered show={editShow} onHide={editHandleClose}>
                    <ChangeCategory
                        state={editHandleClose}
                        category={category}
                        setCategory={setCategory}
                        getCategories={getCategories}
                        method="PUT"
                        title="Edit category"
                    />
                </Modal>
                {/* Main table */}
                <div className="container mt-5">
                    <Table className="table table-striped auto__table text-center" striped bordered hover size="lg">
                        <thead>
                            <tr>
                                <th>
                                    Id
                                    <Button className="btn-light" onClick={() => sortByName()}>
                                        <i className="fa-solid fa-arrows-up-down"></i>
                                    </Button>
                                </th>
                                <th>
                                    Name
                                    <Button className="btn-light">
                                        <i className="fa-solid fa-arrows-up-down"></i>
                                    </Button>
                                </th>
                                <th>
                                    Type
                                    <Button className="btn-light" onClick={() => sortById()}>
                                        <i className="fa-solid fa-arrows-up-down"></i>
                                    </Button>
                                </th>
                                <th>
                                    Danger
                                    <Button className="btn-light">
                                        <i className="fa-solid fa-arrows-up-down"></i>
                                    </Button>
                                </th>
                                <th>Options</th>
                            </tr>
                        </thead>
                        <tbody>
                            {categories.map(e => (
                                <tr key={e.Id}>
                                    <td>{e.Id}</td>
                                    <td>{e.Name}</td>
                                    <td>{e.Type}</td>
                                    <td>{e.Danger}</td>
                                    <td>
                                        <Button className="m-2"  onClick={() => editModalShow(e)} variant="outline-dark">
                                            Edit
                                        </Button>
                                        <Button className="m-2"  onClick={() => deleteClick(e.Id)} variant="outline-danger">
                                            Delete
                                        </Button>
                                    </td>
                                </tr>
                            ))}
                        </tbody>
                    </Table>
                </div>
                <button
                    type="button"
                    className="btn btn-dark m-2 float-end"
                    // Click will trigger modal
                    data-bs-toggle="modal"
                    // Id of modal to be triggered
                    data-bs-target="#exampleModal"
                    onClick={() => this.addClick()}>
                    Add ingridient data
                </button>
            </div>
        </div>
	);
}

export default Categories;
