package com.desafio.codechallenge.model;

public class UserTransferencia
{
	private String cpf;

    private String cpfDestino;

	private long valor;

	//GetSet

	public String getCpf() {
		return cpf;
	}

	public void setCpf(String cpf) {
		this.cpf = cpf;
	}

    public String getCpfDestino() {
		return cpfDestino;
	}

	public void setCpfDestino(String cpfDestino) {
		this.cpfDestino = cpfDestino;
	}

	public long getValor() {
		return valor;
	}

	public void setValor(long valor) {
		this.valor = valor;
	}
}
