import { useState, useEffect } from "react";
import variables from "../../Variables";
import { Modal, Button, Form, Row, Col, Table, InputGroup, FormControl } from "react-bootstrap";
import EditIngridient from "../EditIngridient/EditIngridient";

function Ingridients() {
	const [ingridientsData, setIngridientsData] = useState([]);
	const [validated, setValidated] = useState(false);
	const [show, setShow] = useState(false);
	const [ingridient, setIngridient] = useState();
	const [coloriesSort, setColoriesSort] = useState(false);
	const [search, setSearch] = useState("");
	const [filterParameters, setFilterParameters] = useState({
		Name: false,
		Calories: false,
		Proteins: false,
		Carbohydrates: false,
		Fats: false
	});

	const handleClose = () => setShow(false);
	const handleShow = () => setShow(true);

	function getIngridients() {
		fetch(`${variables.API_URL}/IngridientData`)
			.then(res => res.json())
			.then(data => setIngridientsData(data));
	}

	useEffect(() => {
		getIngridients();
	}, []);

	const handleSubmit = event => {
		const form = event.currentTarget;
		if (!form.checkValidity()) {
			event.preventDefault();
			event.stopPropagation();
		} else {
			event.preventDefault();

			let obj = {
				Name: event.target.elements[0].value,
				Calories: event.target.elements[1].value,
				Proteins: event.target.elements[2].value,
				Carbohydrates: event.target.elements[3].value,
				Fats: event.target.elements[4].value
			};

			fetch(`${variables.API_URL}/IngridientData`, {
				method: "POST",
				headers: {
					Accept: "application/json",
					"Content-Type": "application/json"
				},
				body: JSON.stringify(obj)
			})
				.then(res => res.json())
				.then(
					result => {
						getIngridients();
					},
					error => {
						alert("Failed");
					}
				);
			setValidated(true);
		}
	};

	function deleteClick(id) {
		if (window.confirm("Are you sure?")) {
			fetch(`${variables.API_URL}/IngridientData/${id}`, {
				method: "DELETE",
				headers: {
					Accept: "application/json",
					"Content-Type": "application/json"
				}
			})
				.then(res => res.json())
				.then(
					result => {
						getIngridients();
					},
					error => {
						alert("Failed");
					}
				);
		}
	}

	function editShow(ingridient) {
		handleShow();
		setIngridient(ingridient);
	}

	function sortName() {
		if (filterParameters.Name) {
			ingridientsData.sort((a, b) => a.Name.localeCompare(b.Name));
			setFilterParameters({
				Name: false,
				Calories: filterParameters.Calories,
				Proteins: filterParameters.Proteins,
				Carbohydrates: filterParameters.Carbohydrates,
				Fats: filterParameters.Fats
			});
		} else {
			ingridientsData.sort((b, a) => a.Name.localeCompare(b.Name));
			setFilterParameters({
				Name: true,
				Calories: filterParameters.Calories,
				Proteins: filterParameters.Proteins,
				Carbohydrates: filterParameters.Carbohydrates,
				Fats: filterParameters.Fats
			});
		}
	}

	function sortCalories() {
		if (filterParameters.Calories) {
			ingridientsData.sort((a, b) => (a.Calories > b.Calories ? 1 : b.Calories > a.Calories ? -1 : 0));
			setFilterParameters({
				Name: filterParameters.Name,
				Calories: false,
				Proteins: filterParameters.Proteins,
				Carbohydrates: filterParameters.Carbohydrates,
				Fats: filterParameters.Fats
			});
		} else {
			ingridientsData.sort((a, b) => (a.Calories < b.Calories ? 1 : b.Calories < a.Calories ? -1 : 0));
			setFilterParameters({
				Name: filterParameters.Name,
				Calories: true,
				Proteins: filterParameters.Proteins,
				Carbohydrates: filterParameters.Carbohydrates,
				Fats: filterParameters.Fats
			});
		}
	}

	function sortProteins() {
		if (filterParameters.Proteins) {
			ingridientsData.sort((a, b) => (a.Proteins > b.Proteins ? 1 : b.Proteins > a.Proteins ? -1 : 0));
			setFilterParameters({
				Name: filterParameters.Name,
				Calories: filterParameters.Calories,
				Proteins: false,
				Carbohydrates: filterParameters.Carbohydrates,
				Fats: filterParameters.Fats
			});
		} else {
			ingridientsData.sort((a, b) => (a.Proteins < b.Proteins ? 1 : b.Proteins < a.Proteins ? -1 : 0));
			setFilterParameters({
				Name: filterParameters.Name,
				Calories: filterParameters.Calories,
				Proteins: true,
				Carbohydrates: filterParameters.Carbohydrates,
				Fats: filterParameters.Fats
			});
		}
	}

	function sortCarbohydrates() {
		if (filterParameters.Carbohydrates) {
			ingridientsData.sort((a, b) =>
				a.Carbohydrates > b.Carbohydrates ? 1 : b.Carbohydrates > a.Carbohydrates ? -1 : 0
			);
			setFilterParameters({
				Name: filterParameters.Name,
				Calories: filterParameters.Calories,
				Proteins: filterParameters.Proteins,
				Carbohydrates: false,
				Fats: filterParameters.Fats
			});
		} else {
			ingridientsData.sort((a, b) =>
				a.Carbohydrates < b.Carbohydrates ? 1 : b.Carbohydrates < a.Carbohydrates ? -1 : 0
			);
			setFilterParameters({
				Name: filterParameters.Name,
				Calories: filterParameters.Calories,
				Proteins: filterParameters.Proteins,
				Carbohydrates: true,
				Fats: filterParameters.Fats
			});
		}
	}

	function sortFats() {
		if (filterParameters.Fats) {
			ingridientsData.sort((a, b) => (a.Fats > b.Fats ? 1 : b.Fats > a.Fats ? -1 : 0));
			setFilterParameters({
				Name: filterParameters.Name,
				Calories: filterParameters.Calories,
				Proteins: filterParameters.Proteins,
				Carbohydrates: filterParameters.Carbohydrates,
				Fats: false
			});
		} else {
			ingridientsData.sort((a, b) => (a.Fats < b.Fats ? 1 : b.Fats < a.Fats ? -1 : 0));
			setFilterParameters({
				Name: filterParameters.Name,
				Calories: filterParameters.Calories,
				Proteins: filterParameters.Proteins,
				Carbohydrates: filterParameters.Carbohydrates,
				Fats: true
			});
		}
	}

	function searchName(name) {
		let arr = [];

		for (let i = 0; i < ingridientsData.length; i++) {
			if (ingridientsData[i].Name === name) {
				arr.push(ingridientsData[i]);
			}
		}
		setIngridientsData(arr);
	}

	return (
		<div className="Ingridients">
			<div className="container mt-5 mb-5">
				<Form noValidate validated={validated} onSubmit={handleSubmit}>
					<Row className="mb-3">
						<Form.Group as={Col} controlId="validationCustom01">
							<Form.Control required pattern="^[A-Z][a-z]*$" type="text" placeholder="Name" />
						</Form.Group>
						<Form.Group as={Col} controlId="validationCustom02">
							<Form.Control required pattern="^[1-9][0-9]*$" type="text" placeholder="Calories" />
						</Form.Group>
						<Form.Group as={Col} controlId="validationCustom03">
							<Form.Control required pattern="^[1-9][0-9]*$" type="text" placeholder="Proteins" />
						</Form.Group>
						<Form.Group as={Col} controlId="validationCustom04">
							<Form.Control required pattern="^[1-9][0-9]*$" type="text" placeholder="Carbohydrates" />
						</Form.Group>
						<Form.Group as={Col} controlId="validationCustom05">
							<Form.Control required pattern="^[1-9][0-9]*$" type="text" placeholder="Fats" />
						</Form.Group>
					</Row>
					<Button type="submit">Add</Button>
				</Form>
			</div>
			<div className="container">
				<InputGroup className="mb-3">
					<FormControl
						aria-label="Default"
						placeholder="Search"
						value={search}
						onChange={e => setSearch(e.target.value)}
						aria-describedby="inputGroup-sizing-default"
					/>
					<Button onClick={() => searchName(search)}>Search</Button>
					<Button onClick={() => getIngridients()}>Cancel</Button>
				</InputGroup>
			</div>
			<div className="container">
				<Table className="table" striped bordered hover size="lg">
					<thead>
						<tr>
							<th>
								Name
								<Button className="btn-light" onClick={() => sortName()}>
									<i className="fa-solid fa-arrows-up-down"></i>
								</Button>
							</th>
							<th>
								Calories
								<Button className="btn-light" onClick={() => sortCalories()}>
									<i className="fa-solid fa-arrows-up-down"></i>
								</Button>
							</th>
							<th>
								Proteins
								<Button className="btn-light" onClick={() => sortProteins()}>
									<i className="fa-solid fa-arrows-up-down"></i>
								</Button>
							</th>
							<th>
								Carbohydrates
								<Button className="btn-light" onClick={() => sortCarbohydrates()}>
									<i className="fa-solid fa-arrows-up-down"></i>
								</Button>
							</th>
							<th>
								Fats
								<Button className="btn-light" onClick={() => sortFats()}>
									<i className="fa-solid fa-arrows-up-down"></i>
								</Button>
							</th>
							<th>Action</th>
						</tr>
					</thead>
					<tbody>
						{ingridientsData.map(e => (
							<tr key={e.ID}>
								<td>{e.Name}</td>
								<td>{e.Calories}</td>
								<td>{e.Proteins}</td>
								<td>{e.Carbohydrates}</td>
								<td>{e.Fats}</td>
								<td>
									<Button onClick={() => editShow(e)} variant="outline-dark">
										Edit
									</Button>
									<Button onClick={() => deleteClick(e.ID)} variant="outline-danger">
										Delete
									</Button>
								</td>
							</tr>
						))}
					</tbody>
				</Table>
			</div>
			<Modal size="lg" centered show={show} onHide={handleClose}>
				<EditIngridient
					state={handleClose}
					ingridient={ingridient}
					setIngridient={setIngridient}
					getIngridients={getIngridients}
				/>
			</Modal>
		</div>
	);
}

export default Ingridients;
